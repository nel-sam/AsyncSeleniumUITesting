using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncSeleniumUITesting
{
    [TestClass]
    public class UITestBase
    {
        public void T(Action<IWebDriver> testMethod)
        {
            if (Constants.RunAsync)
            {
                RunTestsAsync(testMethod);
            }
            else
            {
                RunTestsSync(testMethod);
            }
        }

        public void RunTestsAsync(Action<IWebDriver> testMethod)
        {
            List<Task> testTasks = new List<Task>(Constants.TestedBrowsers.Count);

            foreach (var bType in Constants.TestedBrowsers)
            {
                testTasks.Add(Task.Factory.StartNew(() =>
                {
                    // Perform test
                    testMethod(bType);

                    // Close the browser
                    //bType.Quit();
                }));
            }

            Task.WaitAll(testTasks.ToArray());
        }

        public void RunTestsSync(Action<IWebDriver> testMethod)
        {
            foreach (var bType in Constants.TestedBrowsers)
            {
                // Perform test
                testMethod(bType);

                // Close the browser
                //bType.Quit();
            }
        }

        public static void QuitAllBrowsers()
        {
            if (Constants.TestedBrowsers != null)
            {
                // Close out all old browsers if any are found
                foreach (var browser in Constants.TestedBrowsers)
                {
                    browser.Quit();
                }
            }
        }

        public static void InstantiateBrowserObjects()
        {
            QuitAllBrowsers();
            Constants.TestedBrowsers = new List<IWebDriver>();

            if (Constants.TestOnFirefox)
            {
                Constants.TestedBrowsers.Add(new FirefoxDriver());
            }

            if (Constants.TestOnIE)
            {
                var ieOptions = new InternetExplorerOptions();
                ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                Constants.TestedBrowsers.Add(new InternetExplorerDriver(ieOptions));
            }

            if (Constants.TestOnChrome)
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--test-type");
                Constants.TestedBrowsers.Add(new ChromeDriver(chromeOptions));
            }

            if (Constants.TestedBrowsers == null || Constants.TestedBrowsers.Count <= 0)
            {
                throw new Exception("No browsers are enabled. Check app.config to ensure you've enabled at least one browser for testing.");
            }
        }

        public void Wait(int ms = 250)
        {
            System.Threading.Thread.Sleep(ms);
        }

        [TestInitialize]
        public virtual void TestInitialize()
        {
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
        }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            InstantiateBrowserObjects();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            QuitAllBrowsers(); 
        }

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        private TestContext testContextInstance;
    }
}