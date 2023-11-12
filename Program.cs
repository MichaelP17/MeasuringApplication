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

// Initially process the MeasureValueSources once
measureValueSourceProcessController.ProcessMeasureValueSources();

// Now create a timer that periodically refreshes the MeasureValueSources once a day
var measureValueImportInterval = new TimeSpan(1, 0, 0, 0);
var measureValueImportTimer = new MeasureValueSourceImportTimer(measureValueImportInterval.TotalMilliseconds, measureValueSourceProcessController);

