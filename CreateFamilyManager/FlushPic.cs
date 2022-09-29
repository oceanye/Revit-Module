using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using System.IO;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB.Architecture;
using System.Threading;

using System.Xml;

using System.Windows.Forms;

namespace CreateFamilyManager
{

    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.UsingCommandData)]
    class FlushPic : IExternalCommand
    {

       // public string picturePath = @"D:\模型库\图片";

       // public string picturePath = @"D:\学校产品模型库\图片";
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIDocument Uidoc = commandData.Application.ActiveUIDocument;
            Document Doc = Uidoc.Document;



            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            openFileDialog.ShowDialog();

            string PName = openFileDialog.SelectedPath;

            DirectoryInfo dirs = new DirectoryInfo(PName);

            FileInfo[] file = dirs.GetFiles();

            #region 遍历图片文件夹


            if(!Directory.Exists(PName+ @"\图片"))
            {
                Directory.CreateDirectory(PName + @"\图片");
            }
            

            DirectoryInfo dirsPic = new DirectoryInfo(PName + @"\图片");

            FileInfo[] filePic = dirsPic.GetFiles();

            #endregion

            DirectoryInfo[] dirInfo = dirs.GetDirectories();
            try
            {

                for (int i = 0; i < dirInfo.Length; i++)
                {
                    GetMultiNode(dirInfo[i].FullName, Doc,  filePic, PName);
                }



                for (int x = 0; x < file.Length; x++)
                {
                    if (file[x].Name.Split('.').Length <= 2&&(file[x].Extension == ".rvt"|| file[x].Extension == ".rfa"))
                    {
                        int Tag = 0;
                       
                        for (int j=0;j< filePic.Length;j++)
                        {

                            
                           if ( filePic[j].Name.Contains(file[x].Name.Split('.')[0]))
                            {
                                Tag++;
                            }
                             

                        }
                        if(Tag==0)
                        {
                            Document FamilyDoc = null;
                            Family family = null;
                            Transaction getDoc = new Transaction(Doc, "GetPic");
                            #region
                            try
                            {
                                getDoc.Start();
                               

                                FailureHandlingOptions op = getDoc.GetFailureHandlingOptions();
                                MyFailuresPreProcessor failureProcessor = new MyFailuresPreProcessor();

                                op.SetFailuresPreprocessor(failureProcessor);
                                getDoc.SetFailureHandlingOptions(op);


                                if (file[x].Extension == ".rvt")
                                {
                                    FamilyDoc = Doc.Application.OpenDocumentFile(file[x].FullName);
                                }
                                else if (file[x].Extension == ".rfa")
                                {


                                    MyFamilyLoadOptions Mop = new MyFamilyLoadOptions();
                                    


                                    Doc.LoadFamily(file[x].FullName, Mop, out family);

                                   

                                }


                               
                              
                                var statues = getDoc.Commit();

                              

                                if (statues != TransactionStatus.Committed)
                                {
                                    if (failureProcessor.HasError)
                                    {
                                        TaskDialog.Show("Error", failureProcessor.FailureMessage);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                //  MessageBox.Show(ex.Message);
                                if (getDoc.GetStatus() == TransactionStatus.Started)
                                {
                                    getDoc.RollBack();
                                }
                            }
                            #endregion

                            if (family != null)
                            {
                                FamilyDoc = Doc.EditFamily(family);

                                #region
                                if (FamilyDoc != null)
                                {


                                    Transaction GetPic = new Transaction(FamilyDoc, "GetPic");
                                    GetPic.Start();




                                    ICollection<ElementId> ViewList = new FilteredElementCollector(FamilyDoc).OfClass(typeof(Autodesk.Revit.DB.View3D)).ToElementIds();



                                    List<ElementId> list = new List<ElementId>();

                                    Autodesk.Revit.DB.View ViewLevel = null;

                                    if (ViewList.Count != 0)
                                    {


                                        foreach (var viewId in ViewList)
                                        {

                                            Autodesk.Revit.DB.View view = FamilyDoc.GetElement(viewId) as Autodesk.Revit.DB.View;

                                            view.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE).Set(4);

                                            if (view.Name == "预览图")
                                            {
                                                list.Add(viewId);
                                            }


                                        }

                                        if (list.Count == 0)
                                        {
                                            View3D newView;

                                            ViewFamilyType viewtype = new FilteredElementCollector(FamilyDoc).OfClass(typeof(ViewFamilyType))
                                                         .Cast<ViewFamilyType>()
                                                         .FirstOrDefault<ViewFamilyType>(
                                                          y => ViewFamily.ThreeDimensional
                                                           == y.ViewFamily);
                                            newView = View3D.CreateIsometric(FamilyDoc, viewtype.Id);

                                            newView.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE).Set(4);
                                            newView.ViewName = "预览图";


                                            list.Add(newView.Id);
                                        }



                                        ImageExportOptions options = new ImageExportOptions();
                                        options.SetViewsAndSheets(list);

                                        options.ExportRange = ExportRange.SetOfViews;


                                        options.FitDirection = FitDirectionType.Horizontal;

                                        options.ImageResolution = ImageResolution.DPI_600;

                                        //options.PixelSize = 255;
                                        options.HLRandWFViewsFileType = ImageFileType.PNG;

                                        options.ShadowViewsFileType = ImageFileType.PNG;

                                        options.ZoomType = ZoomFitType.Zoom;
                                        options.Zoom = 20;


                                        options.FilePath = PName + @"\图片\" + file[x].Name.Split('.')[0];

                                        FamilyDoc.ExportImage(options);

                                        // ImageView v = ImageView.Create(FamilyDoc, @"E:\test - 三维视图 - {三维}.png");






                                        GetPic.Commit();
                                        //  FamilyDoc.Save();
                                        FamilyDoc.Close();
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region
                                if (FamilyDoc != null)
                                {


                                    Transaction GetPic = new Transaction(FamilyDoc, "GetPic");
                                    GetPic.Start();




                                    ICollection<ElementId> ViewList = new FilteredElementCollector(FamilyDoc).OfClass(typeof(Autodesk.Revit.DB.View3D)).ToElementIds();



                                    List<ElementId> list = new List<ElementId>();

                                    Autodesk.Revit.DB.View ViewLevel = null;

                                    if (ViewList.Count != 0)
                                    {


                                        foreach (var viewId in ViewList)
                                        {

                                            Autodesk.Revit.DB.View view = FamilyDoc.GetElement(viewId) as Autodesk.Revit.DB.View;

                                            view.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE).Set(4);

                                            if (view.Name == "预览图")
                                            {
                                                list.Add(viewId);
                                            }


                                        }

                                        if (list.Count == 0)
                                        {
                                            View3D newView;

                                            ViewFamilyType viewtype = new FilteredElementCollector(FamilyDoc).OfClass(typeof(ViewFamilyType))
                                                         .Cast<ViewFamilyType>()
                                                         .FirstOrDefault<ViewFamilyType>(
                                                          y => ViewFamily.ThreeDimensional
                                                           == y.ViewFamily);
                                            newView = View3D.CreateIsometric(FamilyDoc, viewtype.Id);

                                            newView.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE).Set(4);
                                            newView.ViewName = "预览图";


                                            list.Add(newView.Id);
                                        }



                                        ImageExportOptions options = new ImageExportOptions();
                                        options.SetViewsAndSheets(list);

                                        options.ExportRange = ExportRange.SetOfViews;




                                        options.FitDirection = FitDirectionType.Horizontal;

                                        options.ImageResolution = ImageResolution.DPI_600;

                                        //options.PixelSize = 255;
                                        options.HLRandWFViewsFileType = ImageFileType.PNG;

                                        options.ShadowViewsFileType = ImageFileType.PNG;

                                        options.ZoomType = ZoomFitType.Zoom;
                                        options.Zoom = 20;


                                        options.FilePath = PName + @"\图片\" + file[x].Name.Split('.')[0];

                                        FamilyDoc.ExportImage(options);

                                        // ImageView v = ImageView.Create(FamilyDoc, @"E:\test - 三维视图 - {三维}.png");






                                        GetPic.Commit();
                                        //  FamilyDoc.Save();
                                        FamilyDoc.Close();
                                    }
                                }
                                #endregion
                            }

                        }
                    }
                }


                MessageBox.Show("完成");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            return Result.Succeeded;
        }

        public void GetMultiNode(string Path, Document Doc, FileInfo[] filePic,string PName)
        {

            if (Directory.Exists(Path) == true)
            {
                DirectoryInfo dirs = new DirectoryInfo(Path);


                DirectoryInfo[] dir = dirs.GetDirectories();
                FileInfo[] file = dirs.GetFiles();
                int dircount = dir.Count();
                int filecount = file.Count();

                //List<DirectoryInfo> listDir = new List<DirectoryInfo>();


                //foreach (DirectoryInfo d in dir)
                //{
                //    if ((d.Attributes & FileAttributes.Hidden) == 0)
                //    {
                //        listDir.Add(d);
                //    }
                //}

                for (int i = 0; i < dir.Count(); i++)
                {

                    GetMultiNode(dir[i].FullName, Doc, filePic, PName);

                }

                for (int x = 0; x < file.Length; x++)
                {
                    if (file[x].Name.Split('.').Length <= 2)
                    {

                        int Tag = 0;
                        for (int j = 0; j < filePic.Length; j++)
                        {
                            if (filePic[j].Name.Contains(file[x].Name.Split('.')[0]))
                            {
                                Tag++;
                            }


                        }
                        if (Tag == 0)
                        {
                            Document FamilyDoc = null;
                            Family family = null;

                            Transaction ts = new Transaction(Doc, "ts");
                            ts.Start();

                            if (file[x].Extension == ".rvt")
                            {
                                FamilyDoc = Doc.Application.OpenDocumentFile(file[x].FullName);
                            }
                            else if (file[x].Extension == ".rfa")
                            {
                                MyFamilyLoadOptions Mop = new MyFamilyLoadOptions();
                                
                                Doc.LoadFamily(file[x].FullName, Mop, out family);

                              
                            }
                            ts.Commit();
                            if (family != null)
                            {
                                FamilyDoc = Doc.EditFamily(family);

                                #region

                                if (FamilyDoc != null)
                                {
                                    Transaction GetPic = new Transaction(FamilyDoc, "GetPic");
                                    GetPic.Start();




                                    ICollection<ElementId> ViewList = new FilteredElementCollector(FamilyDoc).OfClass(typeof(Autodesk.Revit.DB.View3D)).ToElementIds();


                                    List<ElementId> list = new List<ElementId>();

                                    Autodesk.Revit.DB.View ViewLevel = null;

                                    if (ViewList.Count != 0)
                                    {


                                        foreach (var viewId in ViewList)
                                        {

                                            Autodesk.Revit.DB.View view = FamilyDoc.GetElement(viewId) as Autodesk.Revit.DB.View;
                                            view.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE).Set(4);

                                            if (view.Name == "预览图")
                                            {
                                                list.Add(viewId);
                                            }


                                        }

                                        if (list.Count == 0)
                                        {
                                            View3D newView;

                                            ViewFamilyType viewtype = new FilteredElementCollector(FamilyDoc).OfClass(typeof(ViewFamilyType))
                                                         .Cast<ViewFamilyType>()
                                                         .FirstOrDefault<ViewFamilyType>(
                                                          y => ViewFamily.ThreeDimensional
                                                           == y.ViewFamily);
                                            newView = View3D.CreateIsometric(FamilyDoc, viewtype.Id);
                                            newView.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE).Set(4);
                                            newView.ViewName = "预览图";


                                            list.Add(newView.Id);
                                        }


                                        ImageExportOptions options = new ImageExportOptions();
                                        options.SetViewsAndSheets(list);

                                        options.ExportRange = ExportRange.SetOfViews;




                                        options.FitDirection = FitDirectionType.Horizontal;

                                        options.ImageResolution = ImageResolution.DPI_600;

                                        //options.PixelSize = 255;
                                        options.HLRandWFViewsFileType = ImageFileType.PNG;

                                        options.ShadowViewsFileType = ImageFileType.PNG;

                                        options.ZoomType = ZoomFitType.Zoom;
                                        options.Zoom = 20;


                                        options.FilePath = PName + @"\图片\" + file[x].Name.Split('.')[0];

                                        FamilyDoc.ExportImage(options);

                                        // ImageView v = ImageView.Create(FamilyDoc, @"E:\test - 三维视图 - {三维}.png");






                                        GetPic.Commit();
                                        //  FamilyDoc.Save();
                                        FamilyDoc.Close();
                                    }
                                }

                                #endregion
                            }
                            else
                            {
                                #region
                                if (FamilyDoc != null)
                                {
                                    Transaction GetPic = new Transaction(FamilyDoc, "GetPic");
                                    GetPic.Start();




                                    ICollection<ElementId> ViewList = new FilteredElementCollector(FamilyDoc).OfClass(typeof(Autodesk.Revit.DB.View3D)).ToElementIds();


                                    List<ElementId> list = new List<ElementId>();

                                    Autodesk.Revit.DB.View ViewLevel = null;

                                    if (ViewList.Count != 0)
                                    {


                                        foreach (var viewId in ViewList)
                                        {

                                            Autodesk.Revit.DB.View view = FamilyDoc.GetElement(viewId) as Autodesk.Revit.DB.View;
                                            view.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE).Set(4);

                                            if (view.Name == "预览图")
                                            {
                                                list.Add(viewId);
                                            }


                                        }

                                        if (list.Count == 0)
                                        {
                                            View3D newView;

                                            ViewFamilyType viewtype = new FilteredElementCollector(FamilyDoc).OfClass(typeof(ViewFamilyType))
                                                         .Cast<ViewFamilyType>()
                                                         .FirstOrDefault<ViewFamilyType>(
                                                          y => ViewFamily.ThreeDimensional
                                                           == y.ViewFamily);
                                            newView = View3D.CreateIsometric(FamilyDoc, viewtype.Id);
                                            newView.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE).Set(4);
                                            newView.ViewName = "预览图";


                                            list.Add(newView.Id);
                                        }


                                        ImageExportOptions options = new ImageExportOptions();
                                        options.SetViewsAndSheets(list);

                                        options.ExportRange = ExportRange.SetOfViews;




                                        options.FitDirection = FitDirectionType.Horizontal;

                                        options.ImageResolution = ImageResolution.DPI_600;

                                        //options.PixelSize = 255;
                                        options.HLRandWFViewsFileType = ImageFileType.PNG;

                                        options.ShadowViewsFileType = ImageFileType.PNG;

                                        options.ZoomType = ZoomFitType.Zoom;
                                        options.Zoom = 20;


                                        options.FilePath = PName + @"\图片\" + file[x].Name.Split('.')[0];

                                        FamilyDoc.ExportImage(options);

                                        // ImageView v = ImageView.Create(FamilyDoc, @"E:\test - 三维视图 - {三维}.png");






                                        GetPic.Commit();
                                        //  FamilyDoc.Save();
                                        FamilyDoc.Close();
                                    }
                                }

                                #endregion
                            }
                        }


                    }
                }

            }

        }

    }
}
