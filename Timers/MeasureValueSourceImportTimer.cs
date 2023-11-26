using MeasuringApplication.Controllers;

namespace MeasuringApplication.Timers
{
    public class MeasureValueSourceImportTimer
    {
        private System.Timers.Timer mTimer;
        private MeasureValueSourceProcessController mSourceProcessController;
        private MeasureValueController mMeasureValueController;

        public MeasureValueSourceImportTimer(double intervalInMiliseconds, MeasureValueSourceProcessController measureValueSourceProcessController, MeasureValueController measureValueController)
        {
            mTimer = new System.Timers.Timer(intervalInMiliseconds);
            mTimer.Elapsed += mTimer_Elapsed;
            mTimer.AutoReset = true;
            mTimer.Enabled = true;

            mSourceProcessController = measureValueSourceProcessController;
            mMeasureValueController = measureValueController;
        }

        private void mTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine($"{nameof(MeasureValueSourceImportTimer)} elapsed, refreshing the Measure Value Sources...");
            ReloadValueSources();

        }

        public void ReloadValueSources()
        {
            var measureValueSources = mSourceProcessController.GetGroupedMeasureValueSources();

            // Prepare the packages so the do not exceed 256 values
            var packageSources = mSourceProcessController.PreparePackages(measureValueSources);

            // Get the mocked Server instances so we can actually call something in this test environment
            var servers = mMeasureValueController.GetServerInstances(mSourceProcessController.MeasureValueSourceFlatModels);

            mMeasureValueController.CreateStationTimers(packageSources, servers);
        }
    }
}
