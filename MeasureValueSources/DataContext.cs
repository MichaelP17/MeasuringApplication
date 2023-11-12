using MeasuringApplication.Interfaces;

namespace MeasuringApplication.MeasureValueSources
{
    public class DataContext : IMeasureValueSourceImport
    {
        /// <summary>
        /// Load the MeasuringValues from a Database
        /// </summary>
        /// <returns>A List of the MeasuringValues</returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<string> GetMeasuringValueSources()
        {
            // This is only a dummy, but in case a Database would be used instead of a list
            // something like this would most likeley be implemented which gets the values from the DB
            throw new NotImplementedException();
        }
    }
}
