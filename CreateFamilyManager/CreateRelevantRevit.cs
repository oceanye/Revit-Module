using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using System.IO;
using Autodesk.Revit.DB.Structure;
using System.Data.SQLite;
using System.Data;
using Autodesk.Revit.DB.Analysis;
using Autodesk.Revit.Attributes;

namespace CreateFamilyManager
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.UsingCommandData)]
    public  class CreateRelevantRevit : IExternalCommand
    {
        // public string filePath = @"D:\学校产品模型库\模块模型";
        public string pathName = null;
        public string DbPath =null;

        public string DFlag;

        public void GetLinkRevit(string Path,Document Doc,SQLiteCommand cmd)
        {


            if (Directory.Exists(Path) == true)
            {
                DirectoryInfo dirsInfo = new DirectoryInfo(Path);


                DirectoryInfo[] dir = dirsInfo.GetDirectories();
                FileInfo[] fl = dirsInfo.GetFiles();


                for (int m = 0; m < fl.Length; m++)
                {
                    if (fl[m].Extension == ".rvt" && fl[m].Name.Split('.').Length <= 2)
                    {
                        cmd.CommandText = "insert into RevitRelevant(RevitName,RevitLinkName) values(@RevitName,@RevitLinkName)";
                        Document familyDcument = Doc.Application.OpenDocumentFile(fl[m].FullName);

                        IList<Element> collter = new FilteredElementCollector(familyDcument).OfCategory(BuiltInCategory.OST_RvtLinks).OfClass(typeof(RevitLinkType)).ToElements();

                        string LinkText ="";
                        foreach (Element ele in collter)
                        {
                            LinkText += ele.Name.Split('.')[0] + ",";
                        }
                        cmd.Parameters.AddWithValue("@RevitName", fl[m].Name.Split('.')[0]);
                        if(LinkText!="")
                        {
                            cmd.Parameters.AddWithValue("@RevitLinkName", LinkText.Substring(0, LinkText.Length - 1));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@RevitLinkName","");
                        }
                       

                        cmd.ExecuteNonQuery();

                        familyDcument.Close();


                    }

                }
                if (dir.Length != 0)
                {
                    for (int n = 0; n < dir.Length; n++)
                    {
                        GetLinkRevit(dir[n].FullName, Doc, cmd);
                    }
                }
            }




        }

       

     

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {


            DateSelect select = new DateSelect();
            select.ShowDialog();
            DFlag = select.DataFlag;

            pathName = select.FileName;

            if (DFlag == "本地")
            {
                DbPath = @" D:\数据库\CenterData.db";
            }
            else
            {
                DbPath = @"Y:\数字化课题\数据库\CenterData.db";
            }

            UIDocument Uidoc = commandData.Application.ActiveUIDocument;
            Document Doc = Uidoc.Document;

            DirectoryInfo dirs = new DirectoryInfo(pathName);

            FileInfo[] file = dirs.GetFiles();

            DirectoryInfo[] dirInfo = dirs.GetDirectories();


            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + DbPath))
            {

                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand();

                cmd.Connection = conn;

                cmd.CommandText = " delete from RevitRelevant";
                cmd.ExecuteNonQuery();

                for (int i = 0; i < file.Length; i++)
                {
                    if (file[i].Extension == ".rvt" && file[i].Name.Split('.').Length <= 2)
                    {
                        cmd.CommandText = "insert into RevitRelevant(RevitName,RevitLinkName) values(@RevitName,@RevitLinkName)";
                        Document familyDocument = Doc.Application.OpenDocumentFile(file[i].FullName);


                        IList<Element> collter = new FilteredElementCollector(familyDocument).OfCategory(BuiltInCategory.OST_RvtLinks).OfClass(typeof(RevitLinkType)).ToElements();

                        string LinkText = null;
                        foreach (Element ele in collter)
                        {
                            LinkText += ele.Name.Split('.')[0] + ",";
                        }
                        cmd.Parameters.AddWithValue("@RevitName", file[i].Name.Split('.')[0]);
                        cmd.Parameters.AddWithValue("@RevitLinkName", LinkText);

                        cmd.ExecuteNonQuery();
                    }
                }

                for (int j = 0; j < dirInfo.Length; j++)
                {

                    GetLinkRevit(dirInfo[j].FullName, Doc, cmd);


                }


                conn.Close();



            }

            return Result.Succeeded;
        }
    }
}
