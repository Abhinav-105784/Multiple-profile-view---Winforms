using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Aec.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Internal;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices.Styles;
[assembly : CommandClass(typeof(multiple_Profile_views.ReturnNames))]
namespace multiple_Profile_views
{
    public class ReturnNames
    {
        
        
        public static void LabelSetid(ComboBox LabelSetIds)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            CivilDocument cdoc = CivilApplication.ActiveDocument;

            LabelSetIds.Items.Clear();
            using(Transaction tr = database.TransactionManager.StartTransaction())
            {
                ProfileLabelSetStyleCollection labelSets = cdoc.Styles.LabelSetStyles.ProfileLabelSetStyles;

                foreach(Autodesk.AutoCAD.DatabaseServices.ObjectId labelSetId in labelSets)
                {
                    ProfileLabelSetStyle profileLabelSets = tr.GetObject(labelSetId,OpenMode.ForRead) as ProfileLabelSetStyle;
                    LabelSetIds.Items.Add(profileLabelSets.Name);
                }
                tr.Commit();
            }

        }

        public static void ProfileStyle(ComboBox ProfileStyle)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            CivilDocument cdoc = CivilApplication.ActiveDocument;

            ProfileStyle.Items.Clear();

            using(Transaction tran = database.TransactionManager.StartTransaction())
            {
                ProfileStyleCollection profiles = cdoc.Styles.ProfileStyles;

                foreach(Autodesk.AutoCAD.DatabaseServices.ObjectId profileId in profiles)
                {
                    ProfileStyle profileStyles= tran.GetObject(profileId, OpenMode.ForRead) as ProfileStyle;
                    ProfileStyle.Items.Add(profileStyles.Name);
                }
                tran.Commit();
            }
        }

        public static void ProfileViewStyle(ComboBox ProfileStyle)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            CivilDocument cdoc = CivilApplication.ActiveDocument;

            ProfileStyle.Items.Clear();

            using (Transaction tran = database.TransactionManager.StartTransaction())
            {
                ProfileViewBandSetStyleCollection profiles = cdoc.Styles.ProfileViewBandSetStyles;

                foreach (Autodesk.AutoCAD.DatabaseServices.ObjectId profileId in profiles)
                {
                    ProfileViewBandSetStyle profileViewBandSetStyles = tran.GetObject(profileId, OpenMode.ForRead) as ProfileViewBandSetStyle;
                    ProfileStyle.Items.Add(profileViewBandSetStyles.Name);
                }
                tran.Commit();
            }
        }
        public static void ProfileViewNameStyle(ComboBox ProfileStyle)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            CivilDocument cdoc = CivilApplication.ActiveDocument;

            ProfileStyle.Items.Clear();

            using (Transaction tran = database.TransactionManager.StartTransaction())
            {
                ProfileViewStyleCollection profiles = cdoc.Styles.ProfileViewStyles;

                foreach (Autodesk.AutoCAD.DatabaseServices.ObjectId profileId in profiles)
                {
                    ProfileViewStyle profileViewStyles = tran.GetObject(profileId, OpenMode.ForRead) as ProfileViewStyle;
                    ProfileStyle.Items.Add(profileViewStyles.Name);
                }
                tran.Commit();
            }
        }


        public static Autodesk.AutoCAD.DatabaseServices.ObjectId LabelSetIdName(string LabelSetId)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            CivilDocument cdoc = CivilApplication.ActiveDocument;

            Autodesk.AutoCAD.DatabaseServices.ObjectId labetSetid = Autodesk.AutoCAD.DatabaseServices.ObjectId.Null;

            using(Transaction trans = database.TransactionManager.StartTransaction())
            {
                foreach (Autodesk.AutoCAD.DatabaseServices.ObjectId ids in cdoc.Styles.LabelSetStyles.ProfileLabelSetStyles)
                {
                    ProfileLabelSetStyle profileStyle = trans.GetObject(ids,OpenMode.ForRead) as ProfileLabelSetStyle;
                    if(profileStyle.Name==LabelSetId)
                    {
                        labetSetid = profileStyle.Id;
                        break;
                    }
                }
                trans.Commit();
            }
            return labetSetid;
        }

        public static Autodesk.AutoCAD.DatabaseServices.ObjectId PrfoileStyleIdName(string profileStyle)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            CivilDocument cdoc = CivilApplication.ActiveDocument;

            Autodesk.AutoCAD.DatabaseServices.ObjectId profileid = Autodesk.AutoCAD.DatabaseServices.ObjectId.Null;

            using (Transaction trans = database.TransactionManager.StartTransaction())
            {
                foreach (Autodesk.AutoCAD.DatabaseServices.ObjectId ids in cdoc.Styles.ProfileStyles)
                {
                    ProfileStyle profileStyleC = trans.GetObject(ids, OpenMode.ForRead) as ProfileStyle;
                    if (profileStyleC.Name == profileStyle)
                    {
                        profileid = profileStyleC.Id;
                        break;
                    }
                }
                trans.Commit();
            }
            return profileid;
        }

        public static Autodesk.AutoCAD.DatabaseServices.ObjectId PrfoileViewStyleIdName(string profileStyle)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            CivilDocument cdoc = CivilApplication.ActiveDocument;

            Autodesk.AutoCAD.DatabaseServices.ObjectId profileid = Autodesk.AutoCAD.DatabaseServices.ObjectId.Null;

            using (Transaction trans = database.TransactionManager.StartTransaction())
            {
                foreach (Autodesk.AutoCAD.DatabaseServices.ObjectId ids in cdoc.Styles.ProfileViewBandSetStyles)
                {
                    ProfileViewBandSetStyle profileStyleC = trans.GetObject(ids, OpenMode.ForRead) as ProfileViewBandSetStyle;
                    if (profileStyleC.Name == profileStyle)
                    {
                        profileid = profileStyleC.Id;
                        break;
                    }
                }
                trans.Commit();
            }
            return profileid;
        }

        public static Autodesk.AutoCAD.DatabaseServices.ObjectId PrfoileViewStyleIdName1(string profileStyle)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            CivilDocument cdoc = CivilApplication.ActiveDocument;

            Autodesk.AutoCAD.DatabaseServices.ObjectId profileid = Autodesk.AutoCAD.DatabaseServices.ObjectId.Null;

            using (Transaction trans = database.TransactionManager.StartTransaction())
            {
                foreach (Autodesk.AutoCAD.DatabaseServices.ObjectId ids in cdoc.Styles.ProfileViewStyles)
                {
                    ProfileViewStyle profileStyleC = trans.GetObject(ids, OpenMode.ForRead) as ProfileViewStyle;
                    if (profileStyleC.Name == profileStyle)
                    {
                        profileid = profileStyleC.Id;
                        break;
                    }
                }
                trans.Commit();
            }
            return profileid;
        }
        public static void LayerList1(ComboBox LayerList)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            Autodesk.AutoCAD.EditorInput.Editor editor = document.Editor;

            using (Transaction tr = database.TransactionManager.StartTransaction())
            {

                LayerTable layerTable = tr.GetObject(database.LayerTableId, OpenMode.ForRead) as LayerTable;
                LayerList.Items.Clear();

                foreach (Autodesk.AutoCAD.DatabaseServices.ObjectId layer in layerTable)
                {
                    LayerTableRecord ltr = tr.GetObject(layer, OpenMode.ForRead) as LayerTableRecord;
                    LayerList.Items.Add(ltr.Name);
                }
            }
        }
        public static Autodesk.AutoCAD.DatabaseServices.ObjectId LayerList2(string layer1)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            Autodesk.AutoCAD.EditorInput.Editor editor = document.Editor;

            Autodesk.AutoCAD.DatabaseServices.ObjectId layerId = Autodesk.AutoCAD.DatabaseServices.ObjectId.Null;
            using (Transaction tr = database.TransactionManager.StartTransaction())
            {

                LayerTable layerTable = tr.GetObject(database.LayerTableId, OpenMode.ForRead) as LayerTable;

                foreach (Autodesk.AutoCAD.DatabaseServices.ObjectId layer in layerTable)
                {
                    LayerTableRecord ltr = tr.GetObject(layer, OpenMode.ForRead) as LayerTableRecord;
                    if (ltr.Name == layer1)
                    {
                        layerId = ltr.Id;
                    }
                }
            }
            return layerId;
        }
    }
}
