using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RView = Autodesk.Revit.DB.View;
using RApplication = Autodesk.Revit.ApplicationServices.Application;

namespace CreateFamilyManager
{
    public partial class PreviewModel : System.Windows.Forms.Form
    {


        private ElementId _currentDBViewId = null;
        private Document _dbDocument = null;
        private RApplication _application = null;
        private UIApplication _uiApplication = null;
        private Document Doc = null;
        public string FileName;

        public PreviewModel(RApplication application, ElementId viewId,string fileName)
        {
            InitializeComponent();
            _application = application;
            FileName = fileName;
            _uiApplication = new UIApplication(application);
            _dbDocument = _uiApplication.ActiveUIDocument.Document;

          

            Doc = _application.OpenDocumentFile(FileName);
            //updateDocumentList(Doc);

            updateViewsList(new UIApplication(Doc.Application).ActiveUIDocument.ActiveView.Id);
           
        }


        private void updateViewsList(ElementId viewId)
        {
            // fill the combobox with printable views <name + id>
            FilteredElementCollector collecotr = new FilteredElementCollector(Doc);
            collecotr.OfClass(typeof(Autodesk.Revit.DB.View));
            IEnumerable<Autodesk.Revit.DB.View> secs = from Element f in collecotr where (f as Autodesk.Revit.DB.View).CanBePrinted == true select f as Autodesk.Revit.DB.View;
            _cbViews.Items.Clear();
            DBViewItem activeItem = null;
            foreach (Autodesk.Revit.DB.View dbView in secs)
            {
                if (viewId == null || viewId.IntegerValue < 0)
                {
                    activeItem = new DBViewItem(dbView, Doc);
                    viewId = dbView.Id;
                }
                if (dbView.Id == viewId)
                {
                    activeItem = new DBViewItem(dbView, Doc);
                    _cbViews.Items.Add(activeItem);
                }
                else
                    _cbViews.Items.Add(new DBViewItem(dbView, Doc));
            }

            foreach (var viewItem in _cbViews.Items)
            {
                DBViewItem dbviewItem = viewItem as DBViewItem;
                if (dbviewItem.Name.Contains("{三维}"))
                {
                    _cbViews.SelectedItem = dbviewItem;
                }
            }
           
        }

     

        private void PreviewModel_Load(object sender, EventArgs e)
        {
           
        }

        private void cbViews_SelIdxChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox cb = sender as System.Windows.Forms.ComboBox;
            if (cb == null)
                return;

            DBViewItem dbItem = cb.SelectedItem as DBViewItem;
            if (dbItem == null)
                return;

            //if (_currentDBViewId == null)
            //    return;

            //RView currentView = _dbDocument.get_Element(_currentDBViewId) as RView;
            //if(currentView == null)
            //    return;

            //if (dbItem.UniqueId.ToLower().CompareTo(currentView.UniqueId.ToLower()) != 0)
            //    return;

            PreviewControl vc = _elementHostWPF.Child as PreviewControl;
            if (vc != null)
                vc.Dispose();
            _elementHostWPF.Child = new PreviewControl(Doc, dbItem.Id);
            _currentDBViewId = dbItem.Id;
        }


  
        public class DBViewItem
        {
            public DBViewItem(RView dbView, Document dbDoc)
            {
                ElementType viewType = dbDoc.GetElement(dbView.GetTypeId()) as ElementType;
                Name = viewType.Name + " " + dbView.Name;
                Id = dbView.Id;
                UniqueId = dbView.UniqueId;
            }

            public override String ToString()
            {
                return Name;
            }

            public String Name { get; set; }

            public ElementId Id { get; set; }

            public String UniqueId { get; set; }
        }


        public class DBDocumentItem
        {
            public DBDocumentItem(String name, Document doc)
            {
                Name = name;
                Document = doc;
                IsNull = false;
            }

            public DBDocumentItem()
            {
                IsNull = true;
            }

            //public override string ToString()
            //{
            //    string ItemName = FileName.Split('\\')[FileName.Split('\\').Length - 1].Split('.')[0];
            //    if (IsNull)
            //        return ItemName;
            //    return Name;
            //}

            public bool IsNull { get; set; }
            public String Name { get; set; }
            public Document Document { get; set; }
        }

       
    }
}
