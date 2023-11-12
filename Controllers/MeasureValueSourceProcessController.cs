using MeasuringApplication.Interfaces;
using MeasuringApplication.Models;

namespace MeasuringApplication.Controllers
{
    /// <summary>
    /// Processes the MeasureValues
    /// </summary>
    public class MeasureValueSourceProcessController
    {
        private IMeasureValueSourceImport mContext;

        public MeasureValueSourceProcessController(IMeasureValueSourceImport context)
        {
            mContext = context;
        }

        public void ProcessMeasureValueSources()
        {
            var measureValueSources = mContext.GetMeasuringValueSources();
            if (measureValueSources == null)
            {
                Console.WriteLine("An Error occured loading the MeasuringValueSources...");
                return;
            }

            // I actually hate this block
            var flatModels = new List<MeasureValueSourceFlatModel>();
            foreach (var measureValueSource in measureValueSources)
            {
                var trimmed = measureValueSource.TrimStart('/');
                var indexOfSlash = trimmed.IndexOf('/');
                var station = trimmed.Substring(0, indexOfSlash);
                trimmed = trimmed.Substring(indexOfSlash, trimmed.Length - indexOfSlash).TrimStart('/');
                indexOfSlash = trimmed.IndexOf("/");
                var server = trimmed.Substring(0, indexOfSlash);
                trimmed = trimmed.Substring(indexOfSlash, trimmed.Length - indexOfSlash).TrimStart('/');
                var path = trimmed;

                var flatModel = new MeasureValueSourceFlatModel()
                {
                    Station = station,
                    Server = server,
                    Path = path,
                };

                flatModels.Add(flatModel);
            }

            var groupedFlatModels = flatModels.GroupBy(x => x.Station).ToList();
        }
    }
}
