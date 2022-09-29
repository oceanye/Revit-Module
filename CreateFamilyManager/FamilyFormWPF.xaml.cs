using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Automation.Peers;

namespace CreateFamilyManager
{
    /// <summary>
    /// FamilyFormWPF.xaml 的交互逻辑
    /// </summary>
    public partial class FamilyFormWPF : Window
    {
        public FamilyFormWPF()
        {
            InitializeComponent();
        }

        public string pathName = @"D:\模型库";
        public string ParaPath = null;
        public  string imagePath = @"C:\ProgramData\Autodesk\Revit\Addins\2018\ICON\";

        public string RevitImagePath = @"D:\模型库\图片\";

        public void SetTreeView(TreeView treeView, string fullPath)
        {
            try
            {

                treeView.Items.Clear();

                DirectoryInfo dirs = new DirectoryInfo(fullPath);
                DirectoryInfo[] dir = dirs.GetDirectories();
                FileInfo[] file = dirs.GetFiles();
                int dirCount = dir.Count();
                int fileCount = file.Count();

                List<DirectoryInfo> listDir = new List<DirectoryInfo>();
                TreeViewItem rootItem = new TreeViewItem();
                rootItem.Header = dirs.Name;

                rootItem.Header= CreateStackPlan(dirs.Name);


                ParaPath = dirs.Parent.FullName;

                treeView.Items.Add(rootItem);

                

                foreach (DirectoryInfo d in dir)
                {
                    if ((d.Attributes & FileAttributes.Hidden) == 0 && d.Name != "图片")
                    {
                        listDir.Add(d);
                    }
                }

                for (int i = 0; i < listDir.Count; i++)
                {
                    TreeViewItem Item = new TreeViewItem();
                    Item.Header = CreateStackPlan(listDir[i].Name);
                    Item.Tag = listDir[i].FullName;

                    Item.MouseDoubleClick -= new MouseButtonEventHandler(TreeViewItem_MouseDoubleClick);
                    Item.MouseDoubleClick += new MouseButtonEventHandler(TreeViewItem_MouseDoubleClick);
                    

                    rootItem.Items.Add(Item);
                    string pathNode = fullPath + "\\" + listDir[i].Name;

                  
                    GetMultiNode(Item, pathNode);



                }

                //for (int j = 0; j < fileCount; j++)
                //{
                //    treeView.Nodes.Add(file[j].Name);
                //}



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void TabItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
 
                SetTreeView(DirectoryView, pathName);
            
        }


        public void GetMultiNode(TreeViewItem treeNode, string Path)
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
                    TreeViewItem Item = new TreeViewItem();
                    Item.Header = CreateStackPlan(listDir[i].Name);
                    Item.Tag = listDir[i].FullName;


                    Item.MouseDoubleClick -= new MouseButtonEventHandler(TreeViewItem_MouseDoubleClick);
                    Item.MouseDoubleClick += new MouseButtonEventHandler(TreeViewItem_MouseDoubleClick);

                    treeNode.Items.Add(Item);

                    string pathNode = Path + "\\" + listDir[i].Name;


                    GetMultiNode(Item, pathNode);


                }

            }

        }


        public StackPanel CreateStackPlan(string ItemName)
        {
            Image icon;
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
          
            //Uncomment this code If you want to add an Image after the Node-HeaderText
            //textBlock = new TextBlock();
            //textBlock.VerticalAlignment = VerticalAlignment.Center;
            //stack.Children.Add(textBlock);
            icon = new Image();
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Height = 20;
            icon.Width = 20;
            icon.Margin = new Thickness(0, 0, 4, 0);
            icon.Source = new BitmapImage(new Uri(imagePath + "主文件.jpeg", UriKind.RelativeOrAbsolute)); 
            stack.Children.Add(icon);
            //Add the HeaderText After Adding the icon
           
            Label label = new Label();
            label.Content = ItemName;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.VerticalContentAlignment= VerticalAlignment.Center;
         
            stack.Children.Add(label);

          
            return stack;
        }


        private void TreeViewItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {


            TreeViewItem treeItem = (TreeViewItem)sender;

           // LView.Items.Clear();

            


            string[] allFile = Directory.GetFiles(treeItem.Tag.ToString());
            string[] iamgeFile = Directory.GetFiles(RevitImagePath);
            List<Image> imageList = new List<Image>();

         
            
          
           


            if (treeItem.BorderThickness == new Thickness(1))
            {
                treeItem.IsSelected = false;

               

                for (int i = 0; i < allFile.Length; i++)
                {

                    if (allFile[i].Split('\\')[allFile[i].Split('\\').Length - 1].Split('.').Length <= 2)
                    {

                        DockPanel dockPan = new DockPanel();
                        dockPan.LastChildFill = false;


                        TextBlock textB = new TextBlock();
                        textB.VerticalAlignment = VerticalAlignment.Center;
                        textB.Margin = new Thickness(0,10,0,0);
                        textB.Height = 27;
                        textB.Width = 105;
                        textB.TextAlignment = TextAlignment.Center;
                        DockPanel.SetDock(textB, Dock.Bottom);

                        Image image = new Image();
                      

                        image.VerticalAlignment = VerticalAlignment.Bottom;
                        image.Height = 100;
                        image.Width = 100;
                        image.Margin = new Thickness(0, 0, 0, 0);
                        string ItemName = allFile[i].Split('\\')[allFile[i].Split('\\').Length - 1].Split('.')[0];
                        for (int j = 0; j < iamgeFile.Length; j++)
                        {
                            if (iamgeFile[j].Split('\\')[iamgeFile[j].Split('\\').Length - 1].Split('.')[0].Contains(ItemName))
                            {
                                image.Source = new BitmapImage(new Uri(iamgeFile[j], UriKind.RelativeOrAbsolute));
                                textB.Text = ItemName;
                            }
                        }

                        DockPanel.SetDock(image, Dock.Top);

                        dockPan.Children.Add(image);
                        dockPan.Children.Add(textB);
                        //imageList.Add(image);

                        wrapP.Children.Add(dockPan);

                        
                    }
                }

              
               
            }

        









        }

        private void LView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
