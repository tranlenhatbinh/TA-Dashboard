using System.Diagnostics;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using Fenton.Selenium.SuperDriver;
using System.Collections.Generic;
using System.Linq;


namespace TADASHBOARRD.Common
{
    class BrowserManager
    {
        #region Methods

        /// <summary>
        /// Method to open the specific browser
        /// </summary>
        public static void OpenBrowser(string browsername)
        {
            if (TestData.runtype.ToUpper() == "LOCAL")
            {
                switch (browsername.ToUpper())
                {
                    case "FIREFOX":
                        WebDriver.driver = new FirefoxDriver();
                        WebDriver.driver.Manage().Window.Maximize();
                        break;
                    case "CHROME":
                        ChromeOptions options = new ChromeOptions();
                        options.AddArguments("--disable-extensions");
                        WebDriver.driver = new ChromeDriver(options);
                        WebDriver.driver.Manage().Window.Maximize();
                        break;
                    case "IE":
                        WebDriver.driver = new InternetExplorerDriver();
                        WebDriver.driver.Manage().Window.Maximize();
                        break;
                    case "EDGE":
                        WebDriver.driver = new EdgeDriver();
                        WebDriver.driver.Manage().Window.Maximize();
                        break;
                    case "SUPERWEBDRIVER":
                        WebDriver.driver = GetDriver(Browser.SuperWebDriver);
                        WebDriver.driver.Manage().Window.Maximize();
                        break;
                    default:
                        WebDriver.driver = new FirefoxDriver();
                        WebDriver.driver.Manage().Window.Maximize();
                        break;
                }
            }
            else if (TestData.runtype.ToUpper() == "GRID")
            {
                switch (browsername.ToUpper())
                {
                    case "FIREFOX":
                        DesiredCapabilities firefoxCapabilities = DesiredCapabilities.Firefox();
                        firefoxCapabilities.SetCapability(CapabilityType.Version, TestData.firefoxVersion);
                        firefoxCapabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
                        WebDriver.driver = new RemoteWebDriver(new Uri(TestData.hub), firefoxCapabilities);
                        WebDriver.driver.Manage().Window.Maximize();
                        break;
                    case "CHROME":
                        DesiredCapabilities chromeCapabilities = DesiredCapabilities.Chrome();
                        chromeCapabilities.SetCapability(CapabilityType.Version, TestData.chromeVersion);
                        chromeCapabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
                        WebDriver.driver = new RemoteWebDriver(new Uri(TestData.hub), DesiredCapabilities.Chrome());
                        WebDriver.driver.Manage().Window.Maximize();
                        break;
                    case "IE":
                        DesiredCapabilities ieCapabilities = DesiredCapabilities.InternetExplorer();
                        ieCapabilities.SetCapability(CapabilityType.Version, TestData.ieVersion);
                        ieCapabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
                        WebDriver.driver = new RemoteWebDriver(new Uri(TestData.hub), ieCapabilities);
                        WebDriver.driver.Manage().Window.Maximize();
                        break;
                    case "EDGE":
                        DesiredCapabilities edgeCapabilities = DesiredCapabilities.Edge();
                        edgeCapabilities.SetCapability(CapabilityType.Version, TestData.edgeVersion);
                        edgeCapabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
                        WebDriver.driver = new RemoteWebDriver(new Uri(TestData.hub), edgeCapabilities);
                        WebDriver.driver.Manage().Window.Maximize();
                        break;
                    case "SUPERWEBDRIVER":
                        WebDriver.driver = GetDriverGrid(Browser.SuperWebDriver);
                        WebDriver.driver.Manage().Window.Maximize();
                        break;
                    default:
                        WebDriver.driver = new RemoteWebDriver(new Uri(TestData.hub), DesiredCapabilities.Firefox());
                        WebDriver.driver.Manage().Window.Maximize();
                        break;
                }
            }
        }

        /// <summary>
        /// Method to create driver for parallel local running
        /// </summary>
        public static IWebDriver GetDriver(Browser browser)
        {
            IWebDriver driver = null;
            switch (browser)
            {
                case Browser.SuperWebDriver:
                    driver = new SuperWebDriver(GetDriverSuite());
                    break;
                case Browser.Chrome:
                    driver = new ChromeDriver();
                    break;
                case Browser.InternetExplorer:
                    driver = new InternetExplorerDriver(new InternetExplorerOptions() { IntroduceInstabilityByIgnoringProtectedModeSettings = true });
                    break;
                default:
                    driver = new FirefoxDriver();
                    break;
            }
            return driver;
        }

        /// <summary>
        /// Get list of drivers for superdriver local
        /// </summary>
        public static IList<IWebDriver> GetDriverSuite()
        {
            // Allow some degree of parallelism when creating drivers, which can be slow
            IList<IWebDriver> drivers = new List<Func<IWebDriver>>
            {
                () => { return GetDriver(Browser.Chrome); },
                () => { return GetDriver(Browser.Firefox); },
                () => { return GetDriver(Browser.InternetExplorer); },
            }.AsParallel().Select(d => d()).ToList();
            return drivers;
        }
        
        /// <summary>
        /// Method to close the browser. If IE, kill process to close the browser
        /// </summary>
        public static void CloseBrowser()
        {
            WebDriver.driver.Manage().Cookies.DeleteAllCookies();
            WebDriver.driver.Quit();
            foreach (Process process in Process.GetProcessesByName("iexplore"))
            {
                process.Kill();
            }
        }

        /// <summary>
        /// Using for Parallel
        /// </summary>
        public enum Browser
        {
            Chrome,
            Firefox,
            InternetExplorer,
            MicrosoftEdge,
            SuperWebDriver
        }

        /// <summary>
        /// Method to create driver for parallel grid running
        /// </summary>
        public static IWebDriver GetDriverGrid(Browser browser)
        {
            IWebDriver driver = GetCapabilityFor(browser);
            driver.Manage().Window.Maximize();
            return driver;
        }

        /// <summary>
        /// Support for GetDriverGrid method
        /// </summary>
        public static IWebDriver GetCapabilityFor(Browser browser)
        {
            var uri = new Uri(TestData.hub);
            IWebDriver driver;
            switch (browser)
            {
                case Browser.SuperWebDriver:
                    driver = new SuperWebDriver(GetDriverSuiteGrid());
                    break;
                case Browser.Chrome:
                    driver = new RemoteWebDriver(uri, DesiredCapabilities.Chrome());
                    break;
                case Browser.InternetExplorer:
                    driver = new RemoteWebDriver(uri, DesiredCapabilities.InternetExplorer());
                    break;
                default:
                    driver = new RemoteWebDriver(uri, DesiredCapabilities.Firefox());
                    break;
            }
            return driver;
        }

        /// <summary>
        /// Get list of drivers for superdriver grid
        /// </summary>
        public static IList<IWebDriver> GetDriverSuiteGrid()
        {
            // Allow some degree of parallelism when creating drivers, which can be slow
            IList<IWebDriver> drivers = new List<Func<IWebDriver>>
            {
                () =>  { return GetCapabilityFor(Browser.Chrome); } ,
                () =>  { return GetCapabilityFor(Browser.Firefox); },
                () => { return GetCapabilityFor(Browser.InternetExplorer); },
            }.AsParallel().Select(d => d()).ToList();
            return drivers;
        }

        #endregion
    }
}
