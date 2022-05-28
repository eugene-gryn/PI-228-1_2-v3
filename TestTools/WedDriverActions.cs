using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Linq;

namespace TestTools
{
    public class WebDriverActions : BaseTest
    {
        public void WaitForElementToBeClickable(By locator)
        {
            var wait = new WebDriverWait(WebDriverFactory.WebDriver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        public void WaitForElementVisible(By locator, int time = 20)
        {
            var wait = new WebDriverWait(WebDriverFactory.WebDriver, TimeSpan.FromSeconds(time));
            wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public void Click(By element)
        {
            try
            {
                WebDriverFactory.WebDriver.FindElement(element).Click();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("element click intercepted") || ex.Message.Contains("Unable to locate element"))
                {
                    WebDriverFactory.WebDriver.FindElement(element).Click();
                }
            }
        }

        public bool ElementExists(By locator)
        {
            return !WebDriverFactory.WebDriver.FindElements(locator).Any();
        }

        public void SendKeys(By element, string text)
        {
            WebDriverFactory.WebDriver.FindElement(element).SendKeys(text);
        }

        public void SwitchFrame(By frame)
        {
            WebDriverFactory.WebDriver.SwitchTo().Frame(WebDriverFactory.WebDriver.FindElement(frame));
        }

        
        public void WaitPageReadyState()
        {
            IWait<IWebDriver> wait = new WebDriverWait(WebDriverFactory.WebDriver, TimeSpan.FromSeconds(30));
            wait.Until(driver1 => ((IJavaScriptExecutor)WebDriverFactory.WebDriver).ExecuteScript("return document.readyState").Equals("complete"));
        }

    }
}