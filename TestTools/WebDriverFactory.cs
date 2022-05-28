using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Text;
using TestTools.Enums;

namespace TestTools
{
    public static class WebDriverFactory
    {
        public static IWebDriver WebDriver { get; set; }

        static WebDriverFactory()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public static IWebDriver CreateWebDriver(Browsers browser = Browsers.Chrome)
        {
            return browser switch
            {
                Browsers.Chrome => CreateChromeDriver(),
                Browsers.HeadlessChrome => CreateHeadlessChromeDriver(),
                //Browsers.Remote => CreateRemoteDriver(remoteDriverUrl),
                _ => throw new ArgumentOutOfRangeException(nameof(browser), browser, null)
            };
        }

        private static IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArguments("window-size=1920,1080");
            WebDriver = new ChromeDriver(options);
            WebDriver.Manage().Window.Maximize();
            return WebDriver;
        }

        private static IWebDriver CreateHeadlessChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArguments("headless");
            options.AddArguments("window-size=1920,1080");
            WebDriver = new ChromeDriver(options);
            return WebDriver;
        }

        private static IWebDriver CreateRemoteDriver(string remoteDriverUrl)
        {
            var options = new ChromeOptions();
            options.AddArguments("--start-maximized");
            options.AddArguments("--disable-infobars");
            options.AddArguments("--window-size=1920,1080");
            options.AddArguments("--disable-extensions");
            options.AddArguments("--disable-dev-shm-usage");
            options.AddArguments("--no-sandbox");
            WebDriver = new RemoteWebDriver(new Uri(remoteDriverUrl), options.ToCapabilities(), TimeSpan.FromMinutes(1));
            return WebDriver;
        }
    }
}