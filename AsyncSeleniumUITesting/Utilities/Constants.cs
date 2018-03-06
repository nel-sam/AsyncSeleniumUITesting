using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace AsyncSeleniumUITesting
{
    public static class Constants
    {
        #region Set in app.config
        public static string Url = ConfigurationManager.AppSettings.Get("Url");
        public static bool RunAsync = Boolean.Parse(ConfigurationManager.AppSettings.Get("RunAsync") ?? "false");
        public static bool TestOnFirefox = Boolean.Parse(ConfigurationManager.AppSettings.Get("Firefox") ?? "false");
        public static bool TestOnIE = Boolean.Parse(ConfigurationManager.AppSettings.Get("IE") ?? "false");
        public static bool TestOnChrome = Boolean.Parse(ConfigurationManager.AppSettings.Get("Chrome") ?? "false");
        #endregion

        // This list gets populated during assembly initialize
        public static List<IWebDriver> TestedBrowsers = null;
    }
}
