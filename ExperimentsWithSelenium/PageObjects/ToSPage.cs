using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace ExperimentsWithSelenium.PageObjects
{
    class ToSPage: ISubPage
    {
        private IWebDriver _Driver;

        public ToSPage(IWebDriver driver)
        {
            this._Driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "/html/body")]
        private IWebElement TosBody;
    
        public bool IsValid()
        {
            return TosBody.Text.StartsWith("This is the terms and conditions document.");
        }
    }
}
