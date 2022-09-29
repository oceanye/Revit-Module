using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
namespace CreateFamilyManager
{
    public partial class DateSelect : Form
    {

        //测试
        public DateSelect()
        {
            InitializeComponent();
        }

        public string DataFlag;
        public string FileName;
        public string fullPath = @"C:\ProgramData\Autodesk\Revit\Addins\2018\模型库\项目文件.txt";

        private void DateSelect_Load(object sender, EventArgs e)
        {
            cmb.SelectedIndex = 0;
            comboBox1.SelectedIndex = 2;
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            DataFlag = cmb.SelectedItem.ToString();
          //  Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateTxt(fullPath);
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        public void CreateTxt(string fullPath)
        {


            FileStream fil = new FileStream(fullPath, FileMode.Open, FileAccess.Read);

            StreamReader sr = new StreamReader(fil);
            string[] srList = sr.ReadToEnd().Split(new char[2] { '\r', '\n' });

            for (int i = 0; i < srList.Length; i++)
            {
                if(srList[i].Length>0&& srList[i].Contains(comboBox1.SelectedItem.ToString()))
                {
                    FileName = srList[i].Split('-')[1];
                }
            }
          

          





        }


    }
}
