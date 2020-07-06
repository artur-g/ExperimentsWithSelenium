using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace ExperimentsWithSelenium.PageObjects
{
    class HomePage
    {
        private IWebDriver _Driver;

        public HomePage(IWebDriver driver)
        {
            this._Driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "number")]
        public IWebElement FactorialTextInput;

        [FindsBy(How = How.Id, Using = "getFactorial")]
        public IWebElement FactorialSubmitButton;

        [FindsBy(How = How.Id, Using = "resultDiv")]
        public IWebElement FactorialResult;
        
        [FindsBy(How = How.PartialLinkText, Using = "Terms and Conditions")]
        private IWebElement TosLink;

        [FindsBy(How = How.PartialLinkText, Using = "Privacy")]
        private IWebElement PrivacyLink;

        [FindsBy(How = How.PartialLinkText, Using = "Qxf2 Services")]
        private IWebElement CopyrightLink;

        public void GoToPage()
        {
            _Driver.Navigate().GoToUrl("http://qainterview.pythonanywhere.com/");
        }

        public ToSPage GoToToSPage()
        {
            TosLink.Click();
            return new ToSPage(_Driver);
        }
        public PrivacyPage GoToPrivacyPage()
        {
            PrivacyLink.Click();
            return new PrivacyPage(_Driver);
        }
        public CopyrightPage GoToCopyrightPage()
        {
            CopyrightLink.Click();
            return new CopyrightPage(_Driver);
        }
    }
}
