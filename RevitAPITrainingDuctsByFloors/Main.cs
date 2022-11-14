using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingDuctsByFloors
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            var levels = new FilteredElementCollector(doc)
                .OfClass(typeof(Level))
                .OfType<Level>()
                .ToList();
            foreach (Level level in levels)
            {
                var ducts = new FilteredElementCollector(doc)
                  .OfClass(typeof(Duct))
                  .OfType<Duct>()
                  .Where(e => e.get_Parameter(BuiltInParameter.RBS_START_LEVEL_PARAM).AsValueString() == level.Name)
                  .Count();

                TaskDialog.Show("шт", $"Этаж - {level.Name} Количество - {ducts}");
            }
            return Result.Succeeded;
        }
    }
}
