using MeasuringApplication.Controllers;

namespace MeasuringApplication.Timers
{
    public class MeasureValueSourceImportTimer
    {
        private System.Timers.Timer mTimer;
        private MeasureValueSourceProcessController mProcessController;

        public MeasureValueSourceImportTimer(double intervalInMiliseconds, MeasureValueSourceProcessController measureValueSourceProcessController)
        {
            mTimer = new System.Timers.Timer(intervalInMiliseconds);
            mTimer.Elapsed += mTimer_Elapsed;
            mTimer.AutoReset = true;
            mTimer.Enabled = true;

            mProcessController = measureValueSourceProcessController;
        }

        private void mTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine($"{nameof(MeasureValueSourceImportTimer)} elapsed, refreshing the Measure Values...");
            mProcessController.ProcessMeasureValueSources();
        }
    }
}
