using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace AsyncSeleniumUITesting
{
    [TestClass]
    public class SampleTestClass : UITestBase
    {        
        [TestMethod]
        public void SampleTestMethod()
        {
            T((IWebDriver b) =>
            {
                List<string> searchStrs = new List<string>() { "Trains", "Planes", "Automobiles" };

                foreach (var searchStr in searchStrs)
                {
                    b.Navigate();
                    b.Url = Constants.Url;

                    // Find the text input element by its name
                    var searchTextBox = b.FindElement(By.Name("q"));

                    // Enter something to search for
                    searchTextBox.SendKeys(searchStr);

                    // Now submit the form. WebDriver will find the form for us from the element
                    searchTextBox.Submit();
                    
                    // Check the title of the page
                    string title = b.WaitForTitleContains(searchStr);
                    Assert.IsTrue(title.Contains(searchStr), "Search string not found in page title.");
                }
            });
        }
        
    }
}
