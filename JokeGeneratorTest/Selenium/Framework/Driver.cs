using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;

namespace JokeGeneratorTests.Selenium.Framework
{
    class Driver
    {
        public static IWebDriver WebDriver { get; set; }

        public static void initialize(string browserName)
        {
            if (browserName.Equals("firefox"))
            {
                //
                // FIX for geckodriver FindElement on Firefox and .netcore being very slow
                // https://github.com/SeleniumHQ/selenium/issues/7840
                //
                var service = FirefoxDriverService.CreateDefaultService();
                service.Host = "::1";
                WebDriver = new FirefoxDriver(service);
            }
            else if (browserName.Equals("chrome"))
                WebDriver = new ChromeDriver();
            else
                throw new NotImplementedException();

            WebDriver.Manage().Window.Maximize();
        }

        public static void ClickRefresh()
        {
            WebDriver.Navigate().Refresh();
        }

        public static void BrowserClose()
        {
            WebDriver.Close();
            WebDriver.Quit();
        }

        public static void Dispose()
        {
            BrowserClose();
            WebDriver = null;
        }

        public static void ImplicitWaitMS(int timeMS)
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(timeMS);
        }
    }
}
