using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using System.Windows.Forms;
using System.Reflection;
using System.Windows.Media.Imaging;
using System;

namespace CreateFamilyManager
{

    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.UsingCommandData)]
    class FamilyManagerUI : IExternalApplication
    {

        string dllPath = Assembly.GetExecutingAssembly().Location;
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {

            string TitleName = "模型库";
            application.CreateRibbonTab(TitleName);


            string imagePath = @"C:\ProgramData\Autodesk\Revit\Addins\2018\ICON\";

            RibbonPanel routePanel = application.CreateRibbonPanel(TitleName, "模型库");

          

            PushButton CreateF = routePanel.AddItem(new PushButtonData("1", "加载模型库", dllPath, "CreateFamilyManager.CreateFrame")) as PushButton;
            CreateF.ToolTip = "加载模型库";

            CreateF.LargeImage = new BitmapImage(new Uri(imagePath + "模型库加载.png", UriKind.RelativeOrAbsolute));



            PushButton FlushPic = routePanel.AddItem(new PushButtonData("2", "更新模型库", dllPath, "CreateFamilyManager.FlushPic")) as PushButton;
            FlushPic.ToolTip = "更新模型库";

            FlushPic.LargeImage = new BitmapImage(new Uri(imagePath + "模型库更新.png", UriKind.RelativeOrAbsolute));

            PushButton PreviewFan= routePanel.AddItem(new PushButtonData("3", "浏览模块库", dllPath, "CreateFamilyManager.CreateFramePreview")) as PushButton;
            PreviewFan.ToolTip = "浏览模块库";

            PreviewFan.LargeImage = new BitmapImage(new Uri(imagePath + "浏览模块库.png", UriKind.RelativeOrAbsolute));


            PushButton CreateRelevantRevit = routePanel.AddItem(new PushButtonData("4", "创建关联数据", dllPath, "CreateFamilyManager.CreateRelevantRevit")) as PushButton;
            CreateRelevantRevit.ToolTip = "创建关联数据";

            CreateRelevantRevit.LargeImage = new BitmapImage(new Uri(imagePath + "关联数据.png", UriKind.RelativeOrAbsolute));




            return Result.Succeeded;
        }
    }
}
