using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_TestProject_cs.Pages
{
    public class LoginPage : HelperMethods
    {
        ChromeDriver chromeDriver;

        public LoginPage(ChromeDriver chromeDriver) { 
            this.chromeDriver = chromeDriver;
        }

        //fields
        IWebElement usernameField => chromeDriver.FindElement(By.Name("login[username]"));
        IWebElement passwordField => chromeDriver.FindElement(By.Name("login[password]"));
        IWebElement loginButton => chromeDriver.FindElement(By.XPath("//button[@type='submit' and span='Sign In']"));


        public void SendUsernameAndPassword(String username, String password)
        {
            usernameField.SendKeys(username);
            passwordField.SendKeys(password);
        }


        public void clickLogin() 
        {
            loginButton.Click();
        }

    }
}
