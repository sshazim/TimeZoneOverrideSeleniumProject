using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using NUnit.Framework;

namespace SeleniumTests
{
    [TestFixture]
    public class EmulateLocation
    {
        private IWebDriver? driver;

        [SetUp]
        public void Setup()
        {
            // Initialize Chrome driver options
            var options = new ChromeOptions();

            // Start Chrome in headless mode (optional)
            // options.AddArgument("--headless");

            // Create a new Chrome session
            driver = new ChromeDriver(options);
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
        }

        [Test]
        [TestCase("Europe/Rome", 41.902783, 12.483333)] // Test case for Italy
        [TestCase("America/Los_Angeles", 34.0522, -118.2437)] // Test case for Los Angeles
        [TestCase("Asia/Tokyo", 35.6895, 139.6917)] // Test case for Tokyo (Example)
        public void TestEmulatedLocationAndTimezone(string timezoneId, double latitude, double longitude)
        {
            // Replace these values with your desired timezone and coordinates
            /*string timezoneId = "America/Los_Angeles";
            double latitude = 34.0522;
            double longitude = -118.2437;*/

            // Initialize Chrome driver options
            var options = new ChromeOptions();

            // Start Chrome in headless mode (optional)
            // options.AddArgument("--headless");

            // Create a new Chrome session
            using var driver = new ChromeDriver(options);
            // Create DevTools session
            var devTools = driver.GetDevToolsSession();

            // Enable necessary DevTools domains
            //devTools.SendCommand(new OpenQA.Selenium.DevTools.V122.Emulation.SetGeolocationOverrideCommandSettings());
            //devTools.SendCommand(new OpenQA.Selenium.DevTools.V122.Network);

            // Set geolocation override
            devTools.SendCommand(new OpenQA.Selenium.DevTools.V122.Emulation.SetGeolocationOverrideCommandSettings
            {
                Latitude = latitude,
                Longitude = longitude
            });

            // Set timezone override
            devTools.SendCommand(new OpenQA.Selenium.DevTools.V122.Emulation.SetTimezoneOverrideCommandSettings
            {
                TimezoneId = timezoneId
            });

            // Navigate to your desired website
            driver.Navigate().GoToUrl("https://www.whatismytimezone.com/");

            // Perform your test case actions here...

            // Remember to disable DevTools domains after use
            /*devTools.SendCommand(OpenQA.Selenium.DevTools.V122.Emulation.disable);
            devTools.SendCommand(ChromeDevTools.Network.DisableCommand);*/
        }

        [Test]
        public void TestMethod1()
        {
            string timezoneId = "America/Los_Angeles";

            // Set up ChromeDriver
            //var driver = new ChromeDriver();

            // Create a DevTools session
            var devTools = ((ChromeDriver)driver).GetDevToolsSession();


            // Set the desired timezone (e.g., "America/New_York")
            var timezoneParams = new Dictionary<string, object> { { "timezoneId", timezoneId } };
            devTools.SendCommand(new OpenQA.Selenium.DevTools.V122.Emulation.SetTimezoneOverrideCommandSettings
            {
                TimezoneId = timezoneId
            });
            // Now your ChromeDriver instance will behave as if it's in the specified timezone
            // You can proceed with your tests
            // Navigate to your desired website
            driver.Navigate().GoToUrl("https://www.whatismytimezone.com/");

        }
    }
}
