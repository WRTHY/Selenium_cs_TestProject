using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drips_p2.Pages
{
    internal class CreateUserPage
    {
        ChromeDriver chromeDriver;

        public CreateUserPage(ChromeDriver chromeDriver)
        {
            this.chromeDriver = chromeDriver;
        }

        //find user fields
        IWebElement firstNameField => chromeDriver.FindElement(By.Name("firstname"));
        IWebElement lastNameField => chromeDriver.FindElement(By.Name("lastname"));
        IWebElement emailField => chromeDriver.FindElement(By.Name("email"));
        IWebElement passwordField => chromeDriver.FindElement(By.Name("password"));
        IWebElement passwordConfirmationField => chromeDriver.FindElement(By.Name("password_confirmation"));
        //login button at bottome of page
        IWebElement loginButton => chromeDriver.FindElement(By.XPath("//button[@type='submit' and span='Create an Account']"));

        //send field values
        public void enterUserInformation(String firstName, String lastName, String email, String password, String passwordConfirm)
        {
            firstNameField.SendKeys(firstName);
            lastNameField.SendKeys(lastName);
            emailField.SendKeys(email);
            passwordField.SendKeys(password);
            passwordConfirmationField.SendKeys(passwordConfirm);//password fields should be the same
        }

    }
}
