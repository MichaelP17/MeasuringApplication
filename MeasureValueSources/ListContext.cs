using IronXL;
using MeasuringApplication.Interfaces;
using System.Reflection;

namespace MeasuringApplication.MeasureValueSources
{
    public class ListContext : IMeasureValueSourceImport
    {
        private readonly string mImportDirectory = @"Import";

        /// <summary>
        /// Load the MeasuringValues from a Excel List
        /// </summary>
        /// <returns>A List of the MeasuringValues</returns>
        public List<string> GetMeasuringValueSources()
        {
            var importDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var importFiles = Directory.GetFiles(Path.Combine(importDirectory, mImportDirectory));

            // In the current case I know that I only have one file so I take the first result
            // If not build a loop that goes through all the files...
            var currentImportFile = importFiles.First();
            var workBook = WorkBook.Load(currentImportFile);
            var measuringValueSources = workBook.DefaultWorkSheet.FilledCells.Select(x => x.StringValue).ToList();

            return measuringValueSources;
        }
    }
}
