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

namespace CreateFamilyManager
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.UsingCommandData)]
    public class CreateFrame : IExternalCommand
    {
              

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument Uidoc = commandData.Application.ActiveUIDocument;
            Document Doc = Uidoc.Document;

            LoadRevitLink loadRevitLink = new LoadRevitLink();
            ExternalEvent loadRevitLinkHandle = ExternalEvent.Create(loadRevitLink);

            LoadRevitLinkAuto loadRevitLinkAuto = new LoadRevitLinkAuto();
            ExternalEvent loadRevitLinkHandleAuto = ExternalEvent.Create(loadRevitLinkAuto);

            RevitPreview revitPreview = new RevitPreview();
            ExternalEvent revitPreviewHandel = ExternalEvent.Create(revitPreview);

            int Flag = 0;

            FamilyFrame fram = new FamilyFrame(Doc, Flag, Uidoc);
            fram.RevitLinkEvent = loadRevitLinkHandle;
            fram.loadRevitLink = loadRevitLink;

            fram.RevitLinkEventAuto = loadRevitLinkHandleAuto;
            fram.loadRevitLinkAuto = loadRevitLinkAuto;

            fram.RevitPreviewHandle = revitPreviewHandel;
            fram.revitPreview = revitPreview;



            fram.Show();




            return Result.Succeeded;
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
