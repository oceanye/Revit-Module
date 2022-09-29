using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Data.SQLite;


namespace CreateFamilyManager
{
    public partial class FamilyFrame : System.Windows.Forms.Form
    {
        public FamilyFrame(Document doc,int tag,UIDocument uidoc)
        {
            Doc = doc;
            Tag = tag;
            Uidoc = uidoc;
            InitializeComponent();


        }

        //public  string pathName = @"D:\模型库";
        public string pathName = null;
        public string ParaPath = null;
        public string filePath = null;
        public string Info = null;

        //public string picturePath = @"D:\模型库\图片";

        //public string FileS = @"D:\学校产品模型库\";

        //public string FileStr = @"D:\学校产品模型库\模块模型\";

        //public string picPath = @"D:\学校产品模型库\图片";

        Document Doc;
        UIDocument Uidoc;
        public int Tag;

        // public string DbPath = @" Y:\数字化课题\数据库\CenterData.db";

        public string DbPath = null;

        public  ExternalEvent RevitLinkEvent;
        public LoadRevitLink loadRevitLink;

        public ExternalEvent RevitLinkEventAuto;
        public LoadRevitLinkAuto loadRevitLinkAuto;

        public ExternalEvent RevitFamilyEvent = null;
        public LoadRevitFamily loadRevitFamily = null;

        public ExternalEvent RevitPreviewHandle= null;
        public RevitPreview revitPreview = null;


    

        public string DFlag = null;

        Dictionary<string, ListView> DirNew = new Dictionary<string, ListView>();

        private void tabControl1_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog openFileDialog = new FolderBrowserDialog();

            //openFileDialog.ShowDialog();

            //pathName = openFileDialog.SelectedPath;

            imageList2.Images.Clear();

            DirectoryInfo dirs = new DirectoryInfo(pathName+@"\图片");

            FileInfo[] file = dirs.GetFiles();

            foreach (FileInfo f in file)
            {

           

                imageList2.Images.Add(f.Name, Image.FromFile(f.FullName));


            }


            DirectoryTree.ImageList = imageList1;

            SetTreeView(DirectoryTree, pathName);

            CreateTxt(pathName);


            //if (tabControl1.SelectedTab.Text == "模型库调用")
            //{
            //    DirectoryInfo dirsSecond = new DirectoryInfo(FileStr);

            //    DirectoryInfo[] dirSecond = dirsSecond.GetDirectories();

            //    for (int m = 0; m < dirSecond.Length; m++)
            //    {
            //        tabControl2.TabPages.Add(dirSecond[m].Name);

            //    }


            //}


        }

        /// <summary>
        /// treeview中添加数据
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="fullPath"></param>
        public void SetTreeView( TreeView treeView,string fullPath)
        {
            try
            {

                treeView.Nodes.Clear();

                DirectoryInfo dirs = new DirectoryInfo(fullPath);
                DirectoryInfo[] dir = dirs.GetDirectories();
                FileInfo[] file = dirs.GetFiles();
                int dirCount = dir.Count();
                int fileCount = file.Count();

                List<DirectoryInfo> listDir = new List<DirectoryInfo>();
                TreeNode rootNode = new TreeNode(dirs.Name);



                ParaPath = dirs.Parent.FullName;

                treeView.Nodes.Add(rootNode);

                foreach(DirectoryInfo d  in dir)
                {
                    if ((d.Attributes & FileAttributes.Hidden) == 0&&d.Name!="图片")
                    {
                        listDir.Add(d);
                    }
                }

                for (int i = 0; i < listDir.Count; i++)
                {

                    rootNode.Nodes.Add(listDir[i].Name);
                    string pathNode = fullPath + "\\" + listDir[i].Name;


                    rootNode.Nodes[i].ImageIndex = 0;

                    GetMultiNode(rootNode.Nodes[i], pathNode);



                }

                //for (int j = 0; j < fileCount; j++)
                //{
                //    treeView.Nodes.Add(file[j].Name);
                //}



            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message);
            }


        }

        public void GetMultiNode(TreeNode treeNode,string Path)
        {

            if (Directory.Exists(Path) == true)
            {
                DirectoryInfo dirs = new DirectoryInfo(Path);


                DirectoryInfo[] dir = dirs.GetDirectories();
                FileInfo[] file = dirs.GetFiles();
                int dircount = dir.Count();
                int filecount = file.Count();
                int sumcount = dircount + filecount;



                List<DirectoryInfo> listDir = new List<DirectoryInfo>();


                foreach (DirectoryInfo d in dir)
                {
                    if ((d.Attributes & FileAttributes.Hidden) == 0)
                    {
                        listDir.Add(d);
                    }
                }

                for (int i = 0; i < listDir.Count; i++)
                {

                    treeNode.Nodes.Add(listDir[i].Name);

                    string pathNode = Path + "\\" + listDir[i].Name;

                   
                    GetMultiNode(treeNode.Nodes[i], pathNode);


                }

            }

        }

        private void DirectoryTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Node.Nodes.Count>0)
            {
                if(e.Node.IsExpanded)
                {
                    e.Node.Collapse();
                }
                else
                {
                    e.Node.Expand();
                }


            }
            else
            {
                try
                {

                    string ssss = ParaPath + e.Node.FullPath;
                    listView1.Clear();
                    string[] allFile = Directory.GetFiles(ParaPath + e.Node.FullPath);

                    listView1.View = System.Windows.Forms.View.LargeIcon;
                  


                    listView1.LargeImageList = imageList2;
                  
                    listView1.Show();
                    int k = 0;
                    for (int i=0;i< allFile.Length;i++)
                    {

                        if (allFile[i].Split('\\')[allFile[i].Split('\\').Length - 1].Split('.').Length <= 2)
                        {
                            k++;
                            
                            string ItemName = allFile[i].Split('\\')[allFile[i].Split('\\').Length - 1].Split('.')[0];
                            listView1.Items.Add(allFile[i].Split('\\')[allFile[i].Split('\\').Length - 1]);

                            for (int j = 0; j < imageList2.Images.Keys.Count; j++)
                            {
                                if (imageList2.Images.Keys[j].Split('.')[0].Contains(ItemName))
                                {
                                    listView1.Items[k-1].ImageIndex = j;
                                }
                            }
                        }                     
                    }

                }
                catch(Exception ex )
                {
                    MessageBox.Show(ex.Message);
                }
            }



        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            ListViewHitTestInfo info = listView1.HitTest(e.X, e.Y);





            DirectoryInfo dirs = new DirectoryInfo(pathName + @"\图片");

            FileInfo[] file = dirs.GetFiles();

            foreach (FileInfo f in file)
            {
                if (info.Item.Text.Contains("MK"))
                {


                    if (f.Name.Split('.')[0].Contains(info.Item.Text.Split('.')[0]))
                    {
                        AddFrame frame = new AddFrame(f.FullName, Doc, info.Item.Text, Uidoc, pathName);

                        frame.loadRevitLinkHandle = RevitLinkEvent;
                        frame.loadLink = loadRevitLink;

                        frame.loadRevitLinkHandleAuto= RevitLinkEventAuto;
                        frame.loadLinkAuto = loadRevitLinkAuto;

                        frame.RevitPrevireHandle = RevitPreviewHandle;
                        frame.RevitPrevire = revitPreview;


                        frame.Show();
                    }
                }
                else
                {
                    if (f.Name.Split('.')[0].Contains(info.Item.Text.Split('.')[0]))
                    {
                        AddFrameOther frame = new AddFrameOther(f.FullName, Doc, info.Item.Text, pathName);
                        frame.loadRevitLinkHandle = RevitLinkEvent;
                        frame.loadLink = loadRevitLink;

                        frame.loadRevitLinkHandleAuto = RevitLinkEventAuto;
                        frame.loadLinkAuto = loadRevitLinkAuto;

                        frame.Show();
                    }
                }
            }
        



          

        }
        

        private void FamilyFrame_Load(object sender, EventArgs e)
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


            imageList3.Images.Clear();

            DirectoryInfo dirs = new DirectoryInfo(pathName+ @"\图片");

            FileInfo[] file = dirs.GetFiles();

            foreach (FileInfo f in file)
            {

                imageList3.Images.Add(f.Name, Image.FromFile(f.FullName));
            }


            tabControl1.SelectedIndex = Tag;



            #region
            DirectoryInfo dirsNew = new DirectoryInfo(pathName + @"\模块模型\");

          

            DirectoryInfo[] dirInfo = dirsNew.GetDirectories();

      
            for (int i = 0; i < dirInfo.Length; i++)
            {

                TabPage tabPage = new TabPage(dirInfo[i].Name);
                tabControl2.TabPages.Add(tabPage);
                ListView lv = new ListView();
                tabPage.Controls.Add(lv);
                lv.Dock = DockStyle.Fill;

                DirNew.Add(dirInfo[i].Name, lv);

            }

            #endregion


            #region 更新数据库
            //createRelevantRevit.filePath = pathName + @"\模块模型";
            //CreateRelevantRevitHandle.Raise();


            #endregion



        }

        private void tabControl2_Click(object sender, EventArgs e)
        {

            #region


            //Dictionary<string, ListView> Dir = new Dictionary<string, ListView>();
            //Dir.Add("行政办公", listView2);
            //Dir.Add("阶梯教室", listView3);
            //Dir.Add("楼梯间", listView4);
            //Dir.Add("普通教室", listView5);
            //Dir.Add("其他功能", listView6);
            //Dir.Add("卫生间", listView7);
            //Dir.Add("宿舍", listView8);
            //Dir.Add("专业教室", listView9);


           // DirectoryInfo dirs = new DirectoryInfo(FileStr);

            DirectoryInfo dirs = new DirectoryInfo(pathName + @"\模块模型\");

            DirectoryInfo[] dir = dirs.GetDirectories();

            for (int i = 0; i < dir.Length; i++)
            {
                if (dir[i].Name == tabControl2.SelectedTab.Text)
                {
                  

                    List<string> allFileSecond = new List<string>();
                    allFileSecond= GetFile(dir[i].FullName);

                    

                    DirNew[tabControl2.SelectedTab.Text].Clear();

                    DirNew[tabControl2.SelectedTab.Text].View = System.Windows.Forms.View.LargeIcon;



                    DirNew[tabControl2.SelectedTab.Text].LargeImageList = imageList3;

                    DirNew[tabControl2.SelectedTab.Text].Show();

                  


                    for (int j = 0; j < allFileSecond.Count; j++)
                    {



                        string ItemName = allFileSecond[j].Split('\\')[allFileSecond[j].Split('\\').Length - 1].Split('.')[0];
                        DirNew[tabControl2.SelectedTab.Text].Items.Add(allFileSecond[j].Split('\\')[allFileSecond[j].Split('\\').Length - 1].Split('.')[0]);

                        for (int k = 0; k < imageList3.Images.Keys.Count; k++)
                        {
                            if (imageList3.Images.Keys[k].Split('.')[0].Contains(ItemName))
                            {
                                DirNew[tabControl2.SelectedTab.Text].Items[j].ImageIndex = k;

                            }
                        }

                    }

                     (DirNew[tabControl2.SelectedTab.Text] as ListView).MouseDoubleClick -= new MouseEventHandler(listView_MouseDoubleClick);
                    (DirNew[tabControl2.SelectedTab.Text] as ListView).MouseDoubleClick += new MouseEventHandler(listView_MouseDoubleClick);


                }
            }


            #endregion
        }



        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            ListView listV = (ListView)sender;

            ListViewHitTestInfo info = listV.HitTest(e.X, e.Y);

            filePath= getFilePath(info.Item.Text.Split('.')[0]);


            Document familyDoc= Doc.Application.OpenDocumentFile(filePath);

            getLinkFile(familyDoc);

           

        }

        /// <summary>
        /// 获得文件路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string getFilePath(string fileName)
        {
            DirectoryInfo dirs = new DirectoryInfo(pathName);

            FileInfo[] file = dirs.GetFiles();
            DirectoryInfo[] dirInfo = dirs.GetDirectories();


            for (int i = 0; i < file.Length; i++)
            {
                if (file[i].Extension == ".rvt")
                {
                    if (file[i].Name.Split('.')[0] == fileName)
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
                    if (file[i].Name.Split('.')[0] == fileName)
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

        /// <summary>
        /// 获得链接文件
        /// </summary>
        /// <param name="familyDoc"></param>
        public void getLinkFile(Document familyDoc )
        {

            FlusePic();
            IList<Element> collter = new FilteredElementCollector(familyDoc).OfCategory(BuiltInCategory.OST_RvtLinks).OfClass(typeof(RevitLinkType)).ToElements();


          

            LinkRevitForm form = new LinkRevitForm();


            form.LView.Clear();

            form.LView.View = System.Windows.Forms.View.LargeIcon;



            form.LView.LargeImageList = imageList3;

           // form.LView.Show();

            for (int j = 0; j < collter.Count; j++)
            {
                RevitLinkType type = familyDoc.GetElement(collter[j].Id) as RevitLinkType;

                form.LView.Items.Add(type.Name.Split('.')[0]);
                for (int k = 0; k < imageList3.Images.Keys.Count; k++)
                {
                    if (imageList3.Images.Keys[k].Split('.')[0].Contains(type.Name.Split('.')[0]))
                    {
                        form.LView.Items[j].ImageIndex = k;

                    }
                }

              
                         
                  
            }
             (form.LView as ListView).MouseClick -= new MouseEventHandler(FamilylistView_MouseDoubleClick);
            (form.LView as ListView).MouseClick += new MouseEventHandler(FamilylistView_MouseDoubleClick);
           // (form.LView as ListView).MouseClick += new MouseEventHandler(RelevantLinkRevit_MouseClick);


            form.ShowDialog();

        }


        private void FamilylistView_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            ListView listV = (ListView)sender;

            ListViewHitTestInfo info = listV.HitTest(e.X, e.Y);

            filePath = getFilePath(info.Item.Text);
            Info = info.Item.Text;

            MouseEventArgs Mouse_e = (MouseEventArgs)e;

            if (Mouse_e.Button == MouseButtons.Right)
            {

                contextMenuStrip1.Show((ListView)sender, new System.Drawing.Point(e.X, e.Y));

                contextMenuStrip1.Items[0].Click -= new EventHandler(ToolStripMenuItem_Click);
                contextMenuStrip1.Items[0].Click += new EventHandler(ToolStripMenuItem_Click);

                contextMenuStrip1.Items[1].Click -= new EventHandler(ToolStripMenuItem_ClickSecond);
                contextMenuStrip1.Items[1].Click += new EventHandler(ToolStripMenuItem_ClickSecond);

               

            }
           


        }


        private void RelevantLinkRevit_MouseClick(object sender, MouseEventArgs e)
        {


            FlusePic();


            RelevantRevit form = new RelevantRevit();


            form.listView.Clear();

            form.listView.View = System.Windows.Forms.View.LargeIcon;



            form.listView.LargeImageList = imageList3;

            ListView listV = (ListView)sender;

            ListViewHitTestInfo info = listV.HitTest(e.X, e.Y);
            string LinkName = info.Item.Text;

            List<string> RevitName = new List<string>();

            MouseEventArgs Mouse_e = (MouseEventArgs)e;
            if (Mouse_e.Button == MouseButtons.Right)
            {
                #region
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + DbPath))
                {

                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand();

                    cmd.Connection = conn;


                    cmd.CommandText = "SELECT*FROM RevitRelevant";


                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    DataTable table = ds.Tables[0];

                    for (int i = 0; i < table.Rows.Count;i++)
                    {
                        if(table.Rows[i].ItemArray[2].ToString().Contains(LinkName))
                        {
                            RevitName.Add(table.Rows[i].ItemArray[1].ToString());
                        }



                    }


                    conn.Close();
                }

                #endregion

                for(int j=0;j< RevitName.Count;j++)
                {


                    form.listView.Items.Add(RevitName[j]);
                    for (int k = 0; k < imageList3.Images.Keys.Count; k++)
                    {
                        if (imageList3.Images.Keys[k].Split('.')[0].Contains(RevitName[j]))
                        {
                            form.listView.Items[j].ImageIndex = k;

                        }
                    }


                }


            }

            form.ShowDialog();


        }

        /// <summary>
        /// 获得链接项目
        /// </summary>
        /// <param name="familyDoc"></param>
        public void GetLinkFamily(Document familyDoc)
        {
            
            IList<Element>  familyInstance= new FilteredElementCollector(familyDoc).OfClass(typeof(FamilyInstance)).ToElements();

            List<Family> familyList = new List<Family>();

            DirectoryInfo dirsPic = new DirectoryInfo(pathName + @"\图片");

            FileInfo[] filePic = dirsPic.GetFiles();

            FamilyForm form = new FamilyForm();

            form.ListV.Clear();

            form.ListV.View = System.Windows.Forms.View.LargeIcon;

            
            form.ListV.LargeImageList = imageList3;


            foreach (Element ele in familyInstance)
            {
                #region
                if (familyDoc.GetElement(ele.GetTypeId()) as FamilySymbol != null)
                {
                   
                   Family fam = (familyDoc.GetElement(ele.GetTypeId()) as FamilySymbol).Family;
                   string ss= fam.Name;
                    if (fam != null)
                    {

                        if (familyList.Count != 0)
                        {
                            int T = 0;
                            for (int i = 0; i < familyList.Count; i++)
                            {
                                if (familyList[i].Name== fam.Name)
                                {
                                    T++;
                                }
                            }

                            if(T==0)
                            {
                                familyList.Add(fam);
                            }
                           
                          
                        }
                        else
                        {
                            familyList.Add(fam);
                        }
                    }



                }

                #endregion
            }


            for(int i=0;i< familyList.Count;i++)
            {
                int F = 0;
                for (int j = 0; j < filePic.Length; j++)
                {
                    if (filePic[j].Name.Contains(familyList[i].Name))
                    {
                        F++;
                    }


                }
                if(F==0)
                {
                    if (familyList[i].IsEditable == true)
                    {


                        Document FamilyD = familyDoc.EditFamily(familyList[i]);
                        #region

                        Transaction GetPic = new Transaction(FamilyD, "GetPic");
                        GetPic.Start();


                        ICollection<ElementId> ViewList = new FilteredElementCollector(FamilyD).OfClass(typeof(View3D)).ToElementIds();





                        Autodesk.Revit.DB.View sss = FamilyD.ActiveView;
                        List<ElementId> list = new List<ElementId>();


                        foreach (var viewId in ViewList)
                        {

                            Autodesk.Revit.DB.View view = FamilyD.GetElement(viewId) as Autodesk.Revit.DB.View;
                            view.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE).Set(4);

                            if (view.Name.Contains("预览图"))
                            {
                                list.Add(viewId);
                                break;
                            }


                        }
                        if (list.Count == 0)
                        {
                            View3D newView;

                            ViewFamilyType viewtype = new FilteredElementCollector(FamilyD).OfClass(typeof(ViewFamilyType))
                                         .Cast<ViewFamilyType>()
                                         .FirstOrDefault<ViewFamilyType>(
                                          x => ViewFamily.ThreeDimensional
                                           == x.ViewFamily);
                            newView = View3D.CreateIsometric(FamilyD, viewtype.Id);
                            newView.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE).Set(4);
                            newView.ViewName = "预览图";




                            list.Add(newView.Id);
                        }

                        if (list.Count != 0)
                        {




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


                            options.FilePath = pathName + @"\图片" + @"\" + familyList[i].Name;

                            FamilyD.ExportImage(options);

                            // ImageView v = ImageView.Create(FamilyDoc, @"E:\test - 三维视图 - {三维}.png");


                            GetPic.Commit();
                            //  FamilyDoc.Save();
                            //  FamilyDoc.Close();
                        }
                        #endregion

                    }

                }

            }

            FlusePic();

            for (int i = 0; i < familyList.Count; i++)
            {
                form.ListV.Items.Add(familyList[i].Name);
                for (int k = 0; k < imageList3.Images.Keys.Count; k++)
                {
                    if (imageList3.Images.Keys[k].Split('.')[0].Contains(familyList[i].Name))
                    {
                        form.ListV.Items[i].ImageIndex = k;

                    }
                }
            }

           
            form.ShowDialog();
        }

        public void FlusePic()
        {
            imageList3.Images.Clear();

            DirectoryInfo dirs = new DirectoryInfo(pathName + @"\图片");

            FileInfo[] file = dirs.GetFiles();

            foreach (FileInfo f in file)
            {

                imageList3.Images.Add(f.Name, Image.FromFile(f.FullName));
            }

        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Document familyDoc = Doc.Application.OpenDocumentFile(filePath);

            GetLinkFamily(familyDoc);
        }

        private void ToolStripMenuItem_ClickSecond(object sender, EventArgs e)
        {

            FlusePic();








            RelevantRevit form = new RelevantRevit();


            form.listView.Clear();

            form.listView.View = System.Windows.Forms.View.LargeIcon;



            form.listView.LargeImageList = imageList3;


            string LinkName = Info;

            List<string> RevitName = new List<string>();

            #region
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + DbPath))
            {

                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand();

                cmd.Connection = conn;


                cmd.CommandText = "SELECT*FROM RevitRelevant";


                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                DataTable table = ds.Tables[0];

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i].ItemArray[2].ToString().Contains(LinkName))
                    {
                        RevitName.Add(table.Rows[i].ItemArray[1].ToString());
                    }



                }


                conn.Close();
            }

            #endregion

            for (int j = 0; j < RevitName.Count; j++)
            {


                form.listView.Items.Add(RevitName[j]);
                for (int k = 0; k < imageList3.Images.Keys.Count; k++)
                {
                    if (imageList3.Images.Keys[k].Split('.')[0].Contains(RevitName[j]))
                    {
                        form.listView.Items[j].ImageIndex = k;

                    }
                }


            }

            form.ShowDialog();
        }

        /// <summary>
        /// 创建txt
        /// </summary>
        /// <param name="fullPath"></param>
        public void CreateTxt(string fullPath)
        {

            DirectoryInfo dirs = new DirectoryInfo(fullPath);
            DirectoryInfo[] dir = dirs.GetDirectories();
            FileInfo[] file = dirs.GetFiles();
            List<DirectoryInfo> listDir = new List<DirectoryInfo>();

            if (Directory.Exists(fullPath) == false)
            {

                Directory.CreateDirectory(fullPath);

            }
            if (!File.Exists(fullPath + "/" +"数据" + ".txt"))
            {

                foreach (DirectoryInfo d in dir)
                {
                    if ((d.Attributes & FileAttributes.Hidden) == 0 && d.Name != "图片")
                    {
                        listDir.Add(d);
                    }
                }

                FileStream fil = new FileStream(fullPath + "/" + "数据" + ".txt", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fil);
                for (int i = 0; i < listDir.Count; i++)
                {
                    string pathNode = fullPath + "\\" + listDir[i].Name;
                    sw.WriteLine(listDir[i].Name);

                    TextMUL(pathNode, sw);

                   

                }

                sw.Close();
                fil.Close();
            }
            else
            {
                foreach (DirectoryInfo d in dir)
                {
                    if ((d.Attributes & FileAttributes.Hidden) == 0 && d.Name != "图片")
                    {
                        listDir.Add(d);
                    }
                }

                FileStream fil = new FileStream(fullPath + "/" + "数据" + ".txt", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fil);
                for (int i = 0; i < listDir.Count; i++)
                {
                    string pathNode = fullPath + "\\" + listDir[i].Name;
                    sw.WriteLine(listDir[i].Name);
                    TextMUL(pathNode, sw);



                }

                sw.Close();
                fil.Close();
            }



        }


        public void TextMUL(string Path, StreamWriter sw)
        {

            if (Directory.Exists(Path) == true)
            {
                DirectoryInfo dirs = new DirectoryInfo(Path);


                DirectoryInfo[] dir = dirs.GetDirectories();
                FileInfo[] file = dirs.GetFiles();
                int dircount = dir.Count();
              



                List<DirectoryInfo> listDir = new List<DirectoryInfo>();


                foreach (DirectoryInfo d in dir)
                {
                    if ((d.Attributes & FileAttributes.Hidden) == 0)
                    {
                        listDir.Add(d);
                    }
                }

                for (int i = 0; i < listDir.Count; i++)
                {

                    sw.WriteLine("----"+listDir[i].Name);
                    string pathNode = Path + "\\" + listDir[i].Name;

                    TextMUL( pathNode, sw);

                }
                for(int j=0;j< file.Length;j++)
                {
                    if(file[j].Name.Split('.').Length <= 2)
                    {
                        sw.WriteLine("--------" + file[j].Name);
                    }
                   
                }

            }

        }

        public List<string> GetFile(string Ph)
        {

            List<string> allFileSecond = new List<string>();

         

            string[] allFile1q11 = Directory.GetFileSystemEntries(Ph);

            for (int i=0;i< allFile1q11.Length;i++)
            {
               string[] allFile = Directory.GetFiles(allFile1q11[i]);

                for (int x = 0; x < allFile.Length; x++)
                {
                    if (allFile[x].Split('\\')[allFile[x].Split('\\').Length - 1].Split('.').Length <= 2)
                    {
                        allFileSecond.Add(allFile[x]);
                    }
                }
            }

            return allFileSecond;


        }
    }
}
