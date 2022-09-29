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
using Autodesk.Revit.Attributes;

namespace CreateFamilyManager
{

    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(JournalingMode.NoCommandData)]
    public class LoadRevitFamily : IExternalEventHandler
    {
        public  Document FamilyD = null;
        public string PicPath = null;
        public string PicName = null;
        public void Execute(UIApplication app)
        {
            UIDocument uidoc = app.ActiveUIDocument;
            Document Doc = uidoc.Document;
           
        }

        public string GetName()
        {
            return "this is test";
        }
    }
}
