using OpenQA.Selenium;
using System;

namespace AsyncSeleniumUITesting
{
    public static class Extensions
    {
        public static string GetTitleWait(this IWebDriver browser, string expectedTitleText, int maxWaitSecs = 10)
        {
            // Break the wait into 250 ms increments 
            maxWaitSecs *= 4;

            while (maxWaitSecs-- > 0 && browser.Title != expectedTitleText)
            {
                System.Threading.Thread.Sleep(250);
            }

            if (maxWaitSecs <= 0 && browser.Title != expectedTitleText)
                throw new Exception("Title never become the expected value of " + expectedTitleText);

            return browser.Title;
        }

        public static string WaitForTitleContains(this IWebDriver browser, string expectedContainingText, int maxWaitSecs = 10)
        {
            // Break the wait into 250 ms increments 
            maxWaitSecs *= 4;

            while (maxWaitSecs-- > 0 && !browser.Title.Contains(expectedContainingText))
            {
                System.Threading.Thread.Sleep(250);
            }

            if (maxWaitSecs <= 0 && !browser.Title.Contains(expectedContainingText))
                throw new Exception("Title never contained the expected value of " + expectedContainingText);

            return browser.Title;
        }
    }
}
