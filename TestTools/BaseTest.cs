using log4net;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.IO;

namespace TestTools
{
    public class BaseTest : IDisposable
    {
        public ILog log = LogManager.GetLogger(typeof(BaseTest));
        public AppConfiguration Configuration { get; set; }
        public BaseTest()
        {
            Configuration = JsonConvert.DeserializeObject<AppConfiguration>(File.ReadAllText("configuration.json"));
        }

        public void Dispose()
        {
            try
            {
                if (WebDriverFactory.WebDriver != null)
                {
                    string pathToSaveTheScreenshot = Directory.GetCurrentDirectory() + @$"/Screenshots/";
                    string fullPathToSaveTheScreenshot = $@"{pathToSaveTheScreenshot}/screenshot_{DateTime.Now:dd-MM-yy_HH-mm-ss}.png";

                    Screenshot image = ((ITakesScreenshot)WebDriverFactory.WebDriver).GetScreenshot();

                    if (!Directory.Exists(pathToSaveTheScreenshot))
                    {
                        Directory.CreateDirectory(pathToSaveTheScreenshot);
                    }
                    image.SaveAsFile(fullPathToSaveTheScreenshot);

                    WebDriverFactory.WebDriver.Quit();
                    WebDriverFactory.WebDriver.Dispose();
                    GC.SuppressFinalize(this);
                }
            }
            catch (Exception ex)
            {
                if (!ex.InnerException.Message.Contains("Cannot access a disposed"))
                {
                    throw ex;
                }
            }
        }
    }
}
    
