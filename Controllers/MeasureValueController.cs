using MeasuringApplication.Interfaces;
using MeasuringApplication.Models;
using MeasuringApplication.Server;
using MeasuringApplication.Timers;

namespace MeasuringApplication.Controllers
{
    /// <summary>
    /// Handles logic to determine the optimal request interval for each station
    /// </summary>
    public class MeasureValueController
    {
        /// <summary>
        /// The optimal TimeSpan in which a station periodically should be called
        /// </summary>
        private readonly int mOptimalStationRequestIntervalInMinutes = 15;
        
        /// <summary>
        /// This method was created because we are in a mock demo context and we do not have an actual server.
        /// It reads all the servers from the FlatModels and creates a List of servers based on the FlatModels.
        /// </summary>
        /// <param name="flatModels"></param>
        /// <returns>A List of Servers</returns>
        public List<IServer> GetServerInstances(List<MeasureValueSourceFlatModel> flatModels)
        {
            var servers = new List<IServer>();
            var groupedFlatModels = flatModels.GroupBy(x => x.Server).ToList();
            foreach (var flatModel in groupedFlatModels)
            {
                var server = new MockServer(flatModel.Key);
                servers.Add(server);
            }
            return servers;
        }

        /// <summary>
        /// Calculates the optimal TimeSpan we need to use to call a station and creates a timer for each station that handles the calls
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="servers"></param>
        public void CreateStationTimers(List<Package> packages, List<IServer> servers)
        {
            if (   packages == null || servers == null
                || packages.Count == 0 || servers.Count == 0)
            {
                Console.WriteLine($"Cannot create StationTimers because {nameof(packages)} or {nameof(servers)} was null or empty");
                return;
            }

            var groupedPackages = packages.GroupBy(x => x.Station).ToList();

            foreach (var station in groupedPackages)
            {
                var requestIntervalMinutes = (double)mOptimalStationRequestIntervalInMinutes / (double)station.ToList().Count;
                var furthestPossibleTimeSpan = TimeSpan.FromMinutes(requestIntervalMinutes);

                // Because this is a mocked context and I know that there is only one server I'll explicitly call that one here
                // In a real scenarion we would need to determine the correct server here
                var server = servers.First();

                var stationTimer = new StationTimer(station.Key, furthestPossibleTimeSpan, station.ToList(), server);
            }
        }
    }
}
