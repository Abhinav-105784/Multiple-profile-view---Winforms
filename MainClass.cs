using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Aec.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices;
using ObjectId = Autodesk.AutoCAD.DatabaseServices.ObjectId;

[assembly: CommandClass(typeof(multiple_Profile_views.MainClass))]
namespace multiple_Profile_views
{
    public class MainClass
    {
        [CommandMethod("MultipleProfileViews")]

        public void LoadForm()
        {
            ObjectId[] selectedSurfaces = SelectSurface.SelectionSurface();
            if(selectedSurfaces==null || selectedSurfaces.Length<2)
            {
                return;
            }

            SelectionSet alginments = SelectAlignments.selectionAlignments1();
            if(alginments==null || alginments.Count==0)
            {
                return;
            }
            Select_Profile_View_Properties form = new Select_Profile_View_Properties();
            ReturnNames.LabelSetid(form.E_P_L_S_ComboBox);
            ReturnNames.LabelSetid(form.P_P_L_S_ComboBox);
            ReturnNames.ProfileStyle(form.E_P_S_ComboBox);
            ReturnNames.ProfileStyle(form.P_P_S_ComboBox);
            ReturnNames.ProfileViewNameStyle(form.PV_Style1_Combobox);
            ReturnNames.ProfileViewStyle(form.PV_Style_ComboBox);
            form.SelectedSurfaces = selectedSurfaces;
            form.selectedAlignments = alginments;
            form.ShowDialog();
        }

        public static void Create(ObjectId[] surfaceSelected, SelectionSet AlignmentsSelected, ObjectId PVStyle, ObjectId PVStyle1, ObjectId ProposedPrLabelId, ObjectId ExistingPrLabelId, ObjectId ExistingPrStyle, ObjectId ProposedPrStyle, string PVName)
        {
            Document document = Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            Editor editor = document.Editor;
            CivilDocument civilDoc = CivilApplication.ActiveDocument;

            int num = 1;
            int num1 = 1;
            int num2 = 1;
            try
            {
                using (Transaction tr = database.TransactionManager.StartTransaction())
                {
                    List<string> PVNames = GetAllProfileViewNames();
                    List<string> profiles = GetAllProfileNames();

                    TinSurface existingSurface = tr.GetObject(surfaceSelected[0], OpenMode.ForWrite) as TinSurface;
                    TinSurface proposedSurface = null;
                    if (surfaceSelected.Length>1 && !surfaceSelected[1].IsNull)
                    {
                        proposedSurface = tr.GetObject(surfaceSelected[1], OpenMode.ForWrite) as TinSurface;
                    }
                    PromptPointOptions insertionPoint = new PromptPointOptions("Select 1st Profile View Insertion Point");
                    PromptPointResult pointResult = editor.GetPoint(insertionPoint);
                    Point3d insert = pointResult.Value;
                    int offset = 0;
                    foreach (SelectedObject obj in AlignmentsSelected)
                    {
                        Alignment alignment = tr.GetObject(obj.ObjectId,OpenMode.ForWrite) as Alignment;

                        /*  string pvnameUnique;
                          do
                          {
                              pvnameUnique = $"{PVName}--{num}";
                              num++;
                          }while(PVNames.Contains(pvnameUnique.ToLowerInvariant().Trim()));

                          PVNames.Add(pvnameUnique.ToLowerInvariant().Trim());

                          string existingUniqueName;
                          do
                          {
                              existingUniqueName = $"ExisitngProfile-{num1}";
                              num1++;
                          }while(profiles.Contains(existingUniqueName));
                          profiles.Add(existingUniqueName.ToLowerInvariant().Trim());

                          string proposedUniqueName;
                          do
                          {
                              proposedUniqueName = $"ProposedProfile--{num2}";
                              num2++;
                          }while(profiles.Contains(proposedUniqueName));
                          profiles.Add(proposedUniqueName.ToLowerInvariant().Trim());*/
                        string existingUniqueName = $"ExisitngProfile-{num1}";
                        while (profiles.Contains(existingUniqueName.ToLowerInvariant().Trim()))
                        {
                            num1++;
                            existingUniqueName = $"ExisitngProfile-{num1}";
                        }
                        profiles.Add(existingUniqueName.ToLowerInvariant().Trim());

                        string proposedUniqueName = $"ProposedProfile--{num2}";
                        while (profiles.Contains(proposedUniqueName.ToLowerInvariant().Trim()))
                        {
                            num2++;
                            proposedUniqueName = $"ProposedProfile--{num2}";
                        }
                        profiles.Add(proposedUniqueName.ToLowerInvariant().Trim());

                        string pvnameUnique = $"{PVName}--{num}";
                        while (PVNames.Contains(pvnameUnique.ToLowerInvariant().Trim()))
                        {
                            num++;
                            pvnameUnique = $"{PVName}--{num}";
                        }
                        PVNames.Add(pvnameUnique.ToLowerInvariant().Trim());

                        Profile.CreateFromSurface(existingUniqueName, alignment.ObjectId, existingSurface.ObjectId, existingSurface.LayerId, ExistingPrStyle, ExistingPrLabelId);
                        if (proposedSurface!= null && proposedSurface.ObjectId != ObjectId.Null)
                        {
                            Profile.CreateFromSurface(proposedUniqueName, alignment.ObjectId, proposedSurface.ObjectId, proposedSurface.LayerId, ProposedPrStyle, ProposedPrLabelId);
                        }
                        Point3d currentInsert = new Point3d(insert.X+offset, insert.Y, insert.Z);
                        ProfileView.Create(alignment.ObjectId,currentInsert,pvnameUnique,PVStyle,PVStyle1);
                        offset += (int)(2*alignment.Length);
                        
                    }
                    tr.Commit();
                }
            }
            catch (System.Exception ex)
            {
                editor.WriteMessage($"Error : {ex.Message}");
            }
        }

        public static List<string> GetAllProfileViewNames()
        {
            List<string> profileViewNames = new List<string>();

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord modelSpace = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;

                foreach (ObjectId objId in modelSpace)
                {
                    if (objId.ObjectClass.DxfName == "AECC_PROFILE_VIEW")
                    {
                        ProfileView pv = trans.GetObject(objId, OpenMode.ForRead) as ProfileView;
                        if (pv != null)
                        {
                            profileViewNames.Add(pv.Name.ToLowerInvariant().Trim());
                        }
                    }
                }

                trans.Commit();
            }

            return profileViewNames;
        }

        public static List<string> GetAllProfileNames()
        {
            List<string> profileNames = new List<string>();

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            CivilDocument civDoc = CivilApplication.ActiveDocument;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                // Loop through all alignments
                foreach (ObjectId alignId in civDoc.GetAlignmentIds())
                {
                    Alignment alignment = trans.GetObject(alignId, OpenMode.ForRead) as Alignment;
                    if (alignment == null)
                        continue;

                    // Get the profiles associated with the alignment
                    foreach (ObjectId profId in alignment.GetProfileIds())
                    {
                        Profile profile = trans.GetObject(profId, OpenMode.ForRead) as Profile;
                        if (profile != null)
                        {
                            profileNames.Add(profile.Name.ToLowerInvariant().Trim());
                        }
                    }
                }

                trans.Commit();
            }

            return profileNames;
        }

    }
}
