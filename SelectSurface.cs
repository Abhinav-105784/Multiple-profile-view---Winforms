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
[assembly:CommandClass(typeof(multiple_Profile_views.SelectSurface))]
namespace multiple_Profile_views
{
    internal class SelectSurface
    {
        public static ObjectId[] SelectionSurface()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor editor = doc.Editor;
            Database database = doc.Database;


            PromptEntityOptions selectExisting = new PromptEntityOptions("\nSelect the Existing Surface");
            selectExisting.SetRejectMessage("\nSelected object is not surface");
            selectExisting.AddAllowedClass(typeof(TinSurface), exactMatch: false);

            PromptEntityResult selectionExisting = editor.GetEntity(selectExisting);
            if(selectionExisting.Status != PromptStatus.OK)
            {
                editor.WriteMessage("\nWrong surface selected try again later");
                return null;
            }

            PromptEntityOptions selectProposed = new PromptEntityOptions("\nSelect the Propsed Surface if exist you can skip if not");
            selectProposed.SetRejectMessage("\nSelected object is not surface");
            selectProposed.AllowNone = true;
            selectProposed.AddAllowedClass(typeof(TinSurface), exactMatch: false);

            PromptEntityResult selectionProposed = editor.GetEntity(selectProposed);
            ObjectId proposedId = ObjectId.Null;

            if (selectionProposed.Status == PromptStatus.OK)
            {
                proposedId = selectionProposed.ObjectId;
            }
            else if(selectionProposed.Status == PromptStatus.None)
            {
                editor.WriteMessage("\nNo proposed surface selected. Proceeding with only the existing surface.");
            }
            else
            {
                editor.WriteMessage("\nWrong surface selected try again later");
                return null;
            }
            ObjectId[] ids = new ObjectId[] { selectionExisting.ObjectId, proposedId };
            return ids;
        }

    }
}
