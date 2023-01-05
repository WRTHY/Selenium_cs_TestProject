using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_cs_TestProject.Pages
{
    internal class HeaderPage
    {
        ChromeDriver chromeDriver;

        public HeaderPage(ChromeDriver chromeDriver)
        {
            this.chromeDriver = chromeDriver;
        }

        //fields
        IWebElement searchBar => chromeDriver.FindElement(By.XPath("//input[@id='search']"));

        //search
        public void inputQueryAndSearch(string searchQuery)
        {
            searchBar.SendKeys(searchQuery);
            searchBar.SendKeys(Keys.Enter);
        }
    }
}
