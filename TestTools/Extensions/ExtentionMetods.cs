using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.Extensions
{
    public static class ExtenstionMethods
    {
        public static string GetText(this By element)
        {
            return WebDriverFactory.WebDriver.FindElement(element).Text;
        }

        public static void Click(this By element)
        {
            WebDriverFactory.WebDriver.FindElement(element).Click();
        }
        public static void SendKeys(this By element, string text)
        {
            WebDriverFactory.WebDriver.FindElement(element).SendKeys(text);
        }
    }
}
