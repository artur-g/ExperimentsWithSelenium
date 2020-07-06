using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace ExperimentsWithSelenium.PageObjects
{
    class PrivacyPage: ISubPage
    {
        private IWebDriver _Driver;

        public PrivacyPage(IWebDriver driver)
        {
            this._Driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "/html/body")]
        private IWebElement PrivacyBody;
        public bool IsValid()
        {
            return PrivacyBody.Text.StartsWith("This is the privacy document.");
        }
    }
}
