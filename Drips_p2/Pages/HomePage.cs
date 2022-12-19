using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drips_p2.Pages
{
    public class HomePage
    {
        ChromeDriver chromeDriver;

        public HomePage(ChromeDriver chromeDriver)
        {
            this.chromeDriver = chromeDriver;
        }

        //fields
        IWebElement signInButton => chromeDriver.FindElement(By.LinkText("Sign In"));

        public void clickSignIn()
        {
            signInButton.Click();
        }


    }
}
