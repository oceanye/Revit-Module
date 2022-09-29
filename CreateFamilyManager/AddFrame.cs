using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.IO;
using Autodesk.Revit.UI.Selection;


namespace CreateFamilyManager
{
    public partial class AddFrame : System.Windows.Forms.Form
    {


        
        public AddFrame(string path, Document doc,string Name,UIDocument uidoc,string pName)
        {

            Doc = doc;
            imagePath = path;
            fileName = Name;
            Uidoc = uidoc;
            pathName = pName;

            InitializeComponent();
        }

        public  string  imagePath;
        Document Doc;
        UIDocument Uidoc;
        public string fileName;
        public string filePath;
        public string pathName =null;


        public  ExternalEvent loadRevitLinkHandle = null;
        public LoadRevitLink loadLink = null;

        public ExternalEvent loadRevitLinkHandleAuto = null;
        public LoadRevitLinkAuto loadLinkAuto = null;

        public ExternalEvent RevitPrevireHandle = null;
        public RevitPreview RevitPrevire = null;

        private void AddFrame_Load(object sender, EventArgs e)
        {
            picBox.Image =Image.FromFile(imagePath);

            txtB1.Text = "8.4";
            txtB2.Text = "8.2";
            txtB3.Text = "40";

            

        }

        private void btn2_Click(object sender, EventArgs e)
        {


            filePath= getFilePath(fileName);

            loadLinkAuto.fileName = filePath;

            loadRevitLinkHandleAuto.Raise();



        }


        public string getFilePath(string fileName)
        {
            DirectoryInfo dirs = new DirectoryInfo(pathName);

            FileInfo[] file = dirs.GetFiles();
            DirectoryInfo[] dirInfo = dirs.GetDirectories();


            for(int i=0;i< file.Length;i++)
            {
                if (file[i].Extension == ".rvt")
                {
                    if(file[i].Name.Split('.')[0]== fileName.Split('.')[0])
                    {
                        filePath = file[i].FullName;
                    }
                }
                    
            }

            for(int j=0;j< dirInfo.Length;j++)
            {

                filePath= GetFilePath(dirInfo[j].FullName, fileName);
            }




            return filePath;
        }


        public string  GetFilePath(string FullName, string fileName)
        {
            DirectoryInfo dirs = new DirectoryInfo(FullName);

            FileInfo[] file = dirs.GetFiles();
            DirectoryInfo[] dirInfo = dirs.GetDirectories();


            for (int i = 0; i < file.Length; i++)
            {
                if (file[i].Extension == ".rvt")
                {
                    if (file[i].Name.Split('.')[0] == fileName.Split('.')[0])
                    {
                        filePath = file[i].FullName;
                    }
                }

            }

            for (int j = 0; j < dirInfo.Length; j++)
            {

                filePath= GetFilePath(dirInfo[j].FullName, fileName);
            }


            return filePath;
        }

        private void btn3_Click(object sender, EventArgs e)
        {


           
            filePath = getFilePath(fileName);
            loadLink.fileName = filePath;

            loadRevitLinkHandle.Raise();




        }

        private void btn1_Click(object sender, EventArgs e)
        {
            filePath = getFilePath(fileName);


            RevitPrevire.fileName = filePath;

            RevitPrevireHandle.Raise();







        }
    }
}
