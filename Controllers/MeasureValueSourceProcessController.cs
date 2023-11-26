using MeasuringApplication.Interfaces;
using MeasuringApplication.Models;

namespace MeasuringApplication.Controllers
{
    /// <summary>
    /// Processes the Sources for the MeasureValues
    /// </summary>
    public class MeasureValueSourceProcessController
    {
        private IMeasureValueSourceImport mContext;

        public List<MeasureValueSourceFlatModel> MeasureValueSourceFlatModels { get; set; }

        public MeasureValueSourceProcessController(IMeasureValueSourceImport context)
        {
            mContext = context;
            MeasureValueSourceFlatModels = new List<MeasureValueSourceFlatModel>();
        }

        /// <summary>
        /// Group FlatModels by Station
        /// </summary>
        /// <returns></returns>
        public List<IGrouping<string, MeasureValueSourceFlatModel>> GetGroupedMeasureValueSources()
        {
            LoadMeasureValueSourceFlatModels();
            var groupedFlatModels = MeasureValueSourceFlatModels.GroupBy(x => x.Station).ToList();
            return groupedFlatModels;
        }

        /// <summary>
        /// Get Measuring Value Sources and translate them into FlatModels
        /// </summary>
        /// <param name="forceInit"></param>
        public void LoadMeasureValueSourceFlatModels(bool forceInit = false)
        {
            if (!forceInit && MeasureValueSourceFlatModels.Count != 0)
            {
                return;
            }

            var measureValueSources = mContext.GetMeasuringValueSources();
            if (measureValueSources == null)
            {
                Console.WriteLine("An Error occured loading the MeasuringValueSources...");
                return;
            }

            // I am not proud about this block but currently I had no idea how to do this better
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

            MeasureValueSourceFlatModels = flatModels;
        }

        public List<Package> PreparePackages(List<IGrouping<string, MeasureValueSourceFlatModel>> groupedFlatModels)
        {
            var packages = new List<Package>();
            foreach (var grouping in groupedFlatModels)
            {
                var chunkedGroup = grouping.Chunk(256).ToList();
                chunkedGroup.ForEach(x => packages.Add(new Package()
                {
                    Station = grouping.Key,
                    MeasureValueSources = x.ToList(),
                }));
            }
            return packages;
        }
    }
}
