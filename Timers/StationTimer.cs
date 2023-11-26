using MeasuringApplication.Interfaces;
using MeasuringApplication.Models;

namespace MeasuringApplication.Timers
{
    public class StationTimer
    {
        private readonly string mStationName;
        private readonly System.Timers.Timer mTimer;
        private readonly List<Package> mPackages;
        private readonly IServer mServer;
        private int mIndex;

        private int Index 
        { 
            get => mIndex;
            set
            { 
                if (value > mPackages.Count - 1)
                {
                    mIndex = 0;
                    return;
                }
                mIndex = value; 
            }
        }

        public StationTimer(string stationName, TimeSpan timeSpan, List<Package> packages, IServer server)
        {
            mStationName = stationName;
            mPackages = packages;
            mServer = server;

            mTimer = new System.Timers.Timer(timeSpan);
            mTimer.Elapsed += mTimer_Elapsed;
            mTimer.AutoReset = true;
            mTimer.Enabled = true;
        }

        private void mTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            var packageResult = mServer.GetPackage(mPackages[Index]);
            Index++;
        }
    }
}
