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
    public  class RevitPreview : IExternalEventHandler
    {
        public string fileName;
        public void Execute(UIApplication app)
        {
            UIDocument uidoc = app.ActiveUIDocument;
            Document Doc = uidoc.Document;

            Selection selection = uidoc.Selection;



            try
            {


                Transaction ts = new Transaction(Doc, "ts");
                ts.Start();

                PreviewModel form = new PreviewModel(app.Application, new ElementId(-1), fileName);
                form.ShowDialog();


                ts.Commit();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        public string GetName()
        {
            return "this is test";
        }
    }
}
