using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Civil.DatabaseServices;
[assembly : CommandClass(typeof(multiple_Profile_views.SelectAlignments))]
namespace multiple_Profile_views
{
    internal class SelectAlignments
    {
        public static SelectionSet selectionAlignments1()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor editor = doc.Editor;
            Database database = doc.Database;

            SelectionSet selectionSet = null;

            PromptSelectionOptions promptSelectionOptions = new PromptSelectionOptions();
            promptSelectionOptions.MessageForAdding = "\n Select Alignments";

            PromptSelectionResult promptSelectionResult = editor.GetSelection(promptSelectionOptions);

            if (promptSelectionResult.Status != PromptStatus.OK)
            {
                editor.WriteMessage("Wrong Selections, re-try again");
                return null;
            }
            List<ObjectId> alginmentIds = new List<ObjectId>();

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                foreach (SelectedObject obj in promptSelectionResult.Value)
                {
                    if (obj != null && obj.ObjectId.ObjectClass.IsDerivedFrom(RXObject.GetClass(typeof(Alignment))))
                    {
                        alginmentIds.Add(obj.ObjectId);
                    }
                }
                transaction.Commit();
            }

            if(alginmentIds.Count > 0)
            {
                selectionSet = SelectionSet.FromObjectIds(alginmentIds.ToArray());
                return selectionSet;
            }
            return null;
        }
    }
}
