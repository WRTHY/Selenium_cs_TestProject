using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_TestProject_cs.Pages
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
        IWebElement createAccountButton => chromeDriver.FindElement(By.LinkText("Create an Account"));

        public void clickSignIn()
        {
            signInButton.Click();
        }

        public void clickCreateAccount() {
            createAccountButton.Click();
        }

    }
}
