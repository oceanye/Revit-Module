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


namespace CreateFamilyManager
{
    public partial class AddFrameOther : System.Windows.Forms.Form
    {
        public AddFrameOther(string path, Document doc, string Name, string pName)
        {

            Doc = doc;
            imagePath = path;
            fileName = Name;
            pathName = pName;

            InitializeComponent();
        }

        public string pathName = null;

        public string imagePath;
        Document Doc;
        public string fileName;
        public string filePath;

        public ExternalEvent loadRevitLinkHandle = null;
        public LoadRevitLink loadLink = null;


        public ExternalEvent loadRevitLinkHandleAuto = null;
        public LoadRevitLinkAuto loadLinkAuto = null;

        private void btn3_Click(object sender, EventArgs e)
        {
            filePath = getFilePath(fileName);

            loadLinkAuto.fileName = filePath;

            loadRevitLinkHandleAuto.Raise();
        }

        private void AddFrameOther_Load(object sender, EventArgs e)
        {
            picBox.Image = Image.FromFile(imagePath);
        }


        public string getFilePath(string fileName)
        {
            DirectoryInfo dirs = new DirectoryInfo(pathName);

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

                filePath = GetFilePath(dirInfo[j].FullName, fileName);
            }




            return filePath;
        }


        public string GetFilePath(string FullName, string fileName)
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

                filePath = GetFilePath(dirInfo[j].FullName, fileName);
            }


            return filePath;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            filePath = getFilePath(fileName);
            loadLink.fileName = filePath;

            loadRevitLinkHandle.Raise();


        }
    }
}
