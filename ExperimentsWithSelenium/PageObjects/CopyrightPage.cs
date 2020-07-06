using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace ExperimentsWithSelenium.PageObjects
{
    class CopyrightPage: ISubPage
    {
        private IWebDriver _Driver;

        public CopyrightPage(IWebDriver driver)
        {
            this._Driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "testimonial")]
        private IWebElement TestimonialSection;
        
        public bool IsValid()
        {
            return TestimonialSection.GetAttribute("class").StartsWith("row");
        }
    }
}
