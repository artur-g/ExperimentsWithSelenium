using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.PageObjects;
using ExperimentsWithSelenium.PageObjects;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using ExperimentsWithSelenium.Helpers;

namespace ExperimentsWithSelenium.Tests
{
    class Tests
    {
        private IWebDriver _Driver;
        private OpenQA.Selenium.Support.UI.WebDriverWait _Wait; // collides with SeleniumExtras.WaitHelpers namespace

        public void SetUp()
        {
            /* Logging and console notes
            DesiredCapabilities caps = DesiredCapabilities.chrome();
            LoggingPreferences logPrefs = new LoggingPreferences();
            logPrefs.enable(LogType.BROWSER, Level.ALL);
            caps.setCapability(CapabilityType.LOGGING_PREFS, logPrefs);
            driver = new ChromeDriver(caps);

            var co = new FirefoxOptions();
            co.SetLoggingPreference(LogType.Browser, LogLevel.All);

            var logs = _Driver.Manage().Logs.GetLog(LogType.Browser); throws null
            */

            _Driver = new ChromeDriver();
            _Wait = new OpenQA.Selenium.Support.UI.WebDriverWait(_Driver, new TimeSpan(0,0,10));
            _Driver.Manage().Window.Maximize();
        }

        public List<TestResult> CalculateFactorialUsingHomePage(List<string> inputData)
        {
            var home = new HomePage(_Driver);
            var results = new List<TestResult>();
            home.GoToPage();

            //reset form
            home.FactorialTextInput.Clear();
            home.FactorialTextInput.SendKeys("0");
            home.FactorialSubmitButton.Click();
            _Wait.Until(ExpectedConditions.TextToBePresentInElement(home.FactorialResult, "The factorial of 0 is: 1"));

            foreach (string input in inputData)
            {
                home.FactorialTextInput.Clear();
                home.FactorialTextInput.SendKeys(input);
                home.FactorialSubmitButton.Click();

                try
                {
                    /// Well a hack, should reset form to other state in that case
                    if (!input.Equals("0"))
                    {
                        _Wait.Until(TextToBeNotPresentInElement(home.FactorialResult, "The factorial of 0 is: 1"));
                    }
                    else
                    {
                        Thread.Sleep(200);
                    }
                    results.Add(new TestResult(input, DataCleanUp(home.FactorialResult.Text)));
                }catch(WebDriverTimeoutException e)
                {
                    results.Add(new TestResult(input,e.Message));
                }
                /// reset form and wait for default state, helps with ajax call beeing out of sync
                home.FactorialTextInput.Clear();
                home.FactorialTextInput.SendKeys("0");
                home.FactorialSubmitButton.Click();

                _Wait.Until(ExpectedConditions.TextToBePresentInElement(home.FactorialResult, "The factorial of 0 is: 1"));


            }

            return results;
        }

        public List<TestResult> VisitSubPages()
        {
            var home = new HomePage(_Driver);
            var results = new List<TestResult>();
            home.GoToPage();
            results.Add(new TestResult("Visited Privacy Page", home.GoToPrivacyPage().IsValid().ToString()));
            home.GoToPage();
            results.Add(new TestResult("Visited ToS Page", home.GoToToSPage().IsValid().ToString()));
            home.GoToPage();
            results.Add(new TestResult("Visited Copyright Page", home.GoToCopyrightPage().IsValid().ToString()));
            home.GoToPage();
            return results;
        }

        /// <summary>
        /// My own "ExpectedCondition". Reverse of "TextToBePresentInElement"
        /// SOURCE: https://github.com/DotNetSeleniumTools/DotNetSeleniumExtras/blob/master/src/WaitHelpers/ExpectedConditions.cs
        /// </summary>
        public static Func<IWebDriver, bool> TextToBeNotPresentInElement(IWebElement element, string text)
        {
            return (driver) =>
            {
                try
                {
                    var elementText = element.Text;
                    return !elementText.Contains(text);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };
        }

        

        private string DataCleanUp(string input)
        {
            if (input.StartsWith("The"))
            {
                input = input.Remove(0, input.LastIndexOf(":") + 1);
            }
            return input;
        }

        public void TearDown()
        {
            _Driver.Close();
        }
    }
}
