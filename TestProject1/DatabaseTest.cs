using Microsoft.Extensions.Configuration;
using TrainingAppData.DB.CONTROLLER;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace TrainingApp.Test;

[TestClass]
public class DatabaseTest
{
    [TestMethod]
    public void IsDatabaseExist()
    {
        // Create a new configuration builder
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // Add a JSON configuration file
            .Build(); // Build the configuration

        DatabaseController controller = new DatabaseController();
    }
}
