using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Selenium_TestProject_cs.Pages;

namespace Selenium_TestProject_cs
{
    public class HelperMethods
    {

        /*
         * 
         * helper methods
         * 
         */

        public void helper_CreateAccountSteps(ChromeDriver chromeDriver, String firstName, String lastName, String email, String password)
        {
            HomePage homePage = new HomePage(chromeDriver);
            CreateUserPage createUserPage = new CreateUserPage(chromeDriver);

            //Nav to login page
            homePage.clickCreateAccount();
            createUserPage.enterUserInformation(firstName, lastName, email, password, password);//same password for both boxes
            createUserPage.clickCreateAccountButton();
        }

        public void helper_LoginSteps(ChromeDriver chromeDriver, String username, String password)
        {
            LoginPage loginPage = new LoginPage(chromeDriver);
            HomePage homePage = new HomePage(chromeDriver);

            homePage.clickSignIn();
            loginPage.SendUsernameAndPassword(username, password);
            loginPage.clickLogin(); 
        }

        public void helper_ViewCart(ChromeDriver chromeDriver)
        {
            IWebElement cartButton = chromeDriver.FindElement(By.CssSelector("a[class='action showcart']"));
            cartButton.Click();

            IWebElement viewAndEditCartLink = chromeDriver.FindElement(By.LinkText("View and Edit Cart"));
            viewAndEditCartLink.Click();
        }

        public void helper_AddSingleItemToCart(ChromeDriver chromeDriver, String quantity)
        {
            //find clothing item on main page
            IWebElement ShirtButton = chromeDriver.FindElement(By.CssSelector("img[alt='Radiant Tee']"));
            ShirtButton.Click();


            new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(3)).Until(chromeDriver => chromeDriver.FindElement(By.CssSelector("div[option-label='L']")));

            //select size, color, quantity
            IWebElement ShirtSize = chromeDriver.FindElement(By.CssSelector("div[option-label='L']"));
            IWebElement ShirtColor = chromeDriver.FindElement(By.CssSelector("div[option-label='Purple']"));
            IWebElement ShirtQuantity = chromeDriver.FindElement(By.XPath("//input[@class='input-text qty']"));

            ShirtSize.Click();
            ShirtColor.Click();
            ShirtQuantity.Clear();
            ShirtQuantity.SendKeys(quantity);

            //Hit add to cart button
            IWebElement addToCartButton = chromeDriver.FindElement(By.XPath("//button[@type='submit' and span='Add to Cart']"));
            addToCartButton.Click();
        }

        public void helper_SearchFunctionality(ChromeDriver chromeDriver, String searchQuery)
        {
            IWebElement searchBar = chromeDriver.FindElement(By.XPath("//input[@id='search']"));
            searchBar.SendKeys(searchQuery);
            searchBar.SendKeys(Keys.Enter);
        }

        public void helper_RemoveItemFromCart(ChromeDriver chromeDriver, WebDriverWait wait)
        {
            wait.Until(chromeDriver => chromeDriver.FindElement(By.XPath("//a[@title='Remove item']")));
            IWebElement deleteItem = chromeDriver.FindElement(By.XPath("//a[@title='Remove item']"));
            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            deleteItem.Click();
        }

        public void helper_ProceedToCheckout(ChromeDriver chromeDriver)
        {
            IWebElement CheckoutButton = chromeDriver.FindElement(By.XPath("//button[@data-role='proceed-to-checkout']"));
            CheckoutButton.Click();
        }

        public void helper_CheckoutFieldsShipping(ChromeDriver chromeDriver, String email, String firstName, String lastName, String streetaddress, String city, String state, String postalcode, String country, String phoneNumber)
        {
            //wait.Until(chromeDriver => chromeDriver.FindElement(By.Name("firstname")));//page load
            //wait.Until(chromeDriver => chromeDriver.FindElement(By.XPath("//button[@type='submit' and span='Next']")));//page load

            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            //find fields
            IWebElement emailField = chromeDriver.FindElement(By.Name("username"));
            IWebElement firstNameField = chromeDriver.FindElement(By.Name("firstname"));
            IWebElement lastNameField = chromeDriver.FindElement(By.Name("lastname"));
            IWebElement streetAddressField = chromeDriver.FindElement(By.Name("street[0]"));
            IWebElement cityField = chromeDriver.FindElement(By.Name("city"));
            IWebElement stateField = chromeDriver.FindElement(By.Name("region_id"));
            IWebElement zipField = chromeDriver.FindElement(By.Name("postcode"));
            IWebElement countryField = chromeDriver.FindElement(By.Name("country_id"));
            IWebElement phoneField = chromeDriver.FindElement(By.Name("telephone"));
            IWebElement shippingMethodField = chromeDriver.FindElement(By.CssSelector("input[value='flatrate_flatrate']"));


            //send field values
            emailField.SendKeys(email);
            firstNameField.SendKeys(firstName);
            lastNameField.SendKeys(lastName);
            streetAddressField.SendKeys(streetaddress);
            cityField.SendKeys(city);
            stateField.SendKeys(state);
            zipField.SendKeys(postalcode);
            countryField.SendKeys(country);
            phoneField.SendKeys(phoneNumber);
            shippingMethodField.Click();

            IWebElement nextButton = chromeDriver.FindElement(By.XPath("//button[@type='submit' and span='Next']"));
            nextButton.Click();
        }
    }
}
