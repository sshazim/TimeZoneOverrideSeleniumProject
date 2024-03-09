using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using NUnit.Framework;
using OpenQA.Selenium.DevTools.V122.Emulation;

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
            //driver = new ChromeDriver(options);
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
            // Initialize Chrome driver options
            var options = new ChromeOptions();

            // Start Chrome in headless mode (optional)
            // options.AddArgument("--headless");

            // Create a new Chrome session
            using var driver = new ChromeDriver(options);

            // Create DevTools session
            DevToolsSession devTools = driver.GetDevToolsSession();
            options.AddArgument("--disable-geolocation");

            // Enable necessary DevTools domains
            //devTools.SendCommand(new OpenQA.Selenium.DevTools.V122.Emulation.SetGeolocationOverrideCommandSettings());
            //devTools.SendCommand(new OpenQA.Selenium.DevTools.V122.Network);

            var emulationSettings = new SetGeolocationOverrideCommandSettings
            {
                Latitude = latitude,
                Longitude = longitude,
                Accuracy = 100
            };

            devTools.SendCommand(emulationSettings);

            // Set timezone override
            devTools.SendCommand(new OpenQA.Selenium.DevTools.V122.Emulation.SetTimezoneOverrideCommandSettings
            {
                TimezoneId = timezoneId
            });

            // Navigate to your desired website
            driver.Navigate().GoToUrl("https://www.whatismytimezone.com/");

            driver.Navigate().GoToUrl("https://mylocation.org/");
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

        [Test]
        [TestCase(41.902783, 12.483333)] // Test case for Italy
        [TestCase(34.0522, -118.2437)] // Test case for Los Angeles
        [TestCase(35.6895, 139.6917)] // Test case for Tokyo (Example)
        public void TestMethod2(double latitude, double longitude)
        {
            // Set ChromeOptions
            ChromeOptions options = new ChromeOptions();

            // Disable built-in geolocation
            options.AddArgument("--disable-geolocation"); 

            // Initialize ChromeDriver object
            ChromeDriver driver = new ChromeDriver(options);

            // Create a DevTools session
            IDevTools devToolsDriver = driver as IDevTools;
            DevToolsSession session = devToolsDriver.GetDevToolsSession();
            
            // Set Geolocation values
            var geoLocationOverrideCommandSettings = new SetGeolocationOverrideCommandSettings();
            geoLocationOverrideCommandSettings.Latitude = latitude;
            geoLocationOverrideCommandSettings.Longitude = longitude;
            geoLocationOverrideCommandSettings.Accuracy = 100;

            // Override the GeoLocation value for the current session
            session
                .GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V122.DevToolsSessionDomains>()
                .Emulation
                .SetGeolocationOverride(geoLocationOverrideCommandSettings);

            driver.Url = "https://my-location.org/";

            // Your test logic here
        }

        [Test]
        public static async Task GeoLocation()
        {
            // Set ChromeOptions
            ChromeOptions chromeOptions = new ChromeOptions();

            // Initialize ChromeDriver object
            var driver = new ChromeDriver(chromeOptions);

            // Create a DevTools session
            DevToolsSession devToolsSession = driver.GetDevToolsSession();

            // Set Geolocation values
            var geoLocationOverrideCommandSettings = new SetGeolocationOverrideCommandSettings();
            geoLocationOverrideCommandSettings.Latitude = 51.507351;
            geoLocationOverrideCommandSettings.Longitude = -0.127758;
            geoLocationOverrideCommandSettings.Accuracy = 1;

            // Override the GeoLocation value for the current session
            await devToolsSession
              .GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V122.DevToolsSessionDomains>()
              .Emulation
              .SetGeolocationOverride(geoLocationOverrideCommandSettings);

            driver.Url = "https://my-location.org/";
        }
    }
}