using MeasuringApplication.Controllers;
using MeasuringApplication.Interfaces;
using MeasuringApplication.MeasureValueSources;
using MeasuringApplication.Timers;

// Create an Instance of the IMeasureValueSourceImport interface
// This can be List or Database but in that case we know that we want to use List
// So create a ListContext
IMeasureValueSourceImport measureValueSourceContext = new ListContext();

// Create a MeasureValueSourceProcessController that processes our MeasureValueSources
var measureValueSourceProcessController = new MeasureValueSourceProcessController(measureValueSourceContext);

// Create a MeasureValueController that creates our mocked Servers and will send our requests to the Servers
var measureValueController = new MeasureValueController();

// Now create a timer that periodically refreshes the MeasureValueSources once a day
var measureValueImportInterval = new TimeSpan(1, 0, 0, 0);
var measureValueImportTimer = new MeasureValueSourceImportTimer(measureValueImportInterval.TotalMilliseconds, measureValueSourceProcessController, measureValueController);

// And because the timer does not trigger when initializing we manually trigger the logic once to get going
measureValueImportTimer.ReloadValueSources();

