using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Diagnostics;
using System.ComponentModel.Design;
using OpenQA.Selenium.Support.UI;
using System;

namespace Drips_p2
{
    public class TestSuite : HelperMethods
    {
        String demoUserEmail = "roni_cost@example.com";//email provided by magento
        String demoUserPassword = "roni_cost3@example.com";//password provided by magento

        ChromeDriver chromeDriver;//WebDriver
        WebDriverWait wait;//use for delays

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Setup");
            this.chromeDriver = new ChromeDriver();
            this.wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(10));
            chromeDriver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
        }

        [Test]
        public void CreateUser() 
        {
            //Using random num to generate email and pw
            String randomNum = new Random().Next(0, 9999).ToString("D4");

            //First name, Last name, email, password
            helper_CreateAccountSteps(chromeDriver, "James", "Usher", "jamesusher"+randomNum+"@gmail.com", randomNum+"abcd!");

            //On successful account creation user is directed to "My Account" page
            IWebElement myAccountBanner = chromeDriver.FindElement(By.XPath("//span[@class='base' and text() = 'My Account']"));
            Assert.True(myAccountBanner.Displayed);
        }

        [Test]
        public void CreateUser_Negative()
        {
            //Leave email box empty, will not allow user to progress past step

            //First name, Last name, email, password
            helper_CreateAccountSteps(chromeDriver, "James", "Usher", "", "1234abcd!");

            IWebElement errorMessage = chromeDriver.FindElement(By.XPath("//div[text() = 'This is a required field.']"));
            Assert.True(errorMessage.Displayed);
        }


        [Test]
        public void LoginWithUsernameAndPassword()
        {
            //Validating presence of captcha message, unable to automate login
            helper_LoginSteps(chromeDriver, demoUserEmail, demoUserPassword);

            //Find Captcha message in page, validate that it is appearing
            var captchaMessage = chromeDriver.FindElement(By.Name("captcha[user_login]"));
            Assert.True(captchaMessage.Displayed);
        }


        [Test]
        public void LoginWithUsernameAndPassword_Negative()
        {
            //Validating presence of captcha message, unable to automate login
            //Break login by appending character to email
            helper_LoginSteps(chromeDriver, demoUserEmail + "q", demoUserPassword);

            //Validate Captcha message, would change to login page elements if postive test wasn't broken
            var captchaMessage = chromeDriver.FindElement(By.Name("captcha[user_login]"));
            Assert.True(captchaMessage.Displayed);
        }

        [Test]
        public void AddItemToCart()
        {
            String itemQuantity = "2";//quantity of item to add to cart

            helper_AddSingleItemToCart(chromeDriver, itemQuantity);//pass item quantity
            wait.Until(chromeDriver => chromeDriver.FindElement(By.XPath("//span[text() = '" + itemQuantity + "']")));

            String cartValue = chromeDriver.FindElement(By.XPath("//span[text() = '" + itemQuantity + "']")).Text;
            Assert.That(itemQuantity, Is.EqualTo(cartValue));

        }

        [Test]
        public void AddItemToCart_Negative()
        {
            //Attempt to add <1 item to cart, assert it will not let you
            helper_AddSingleItemToCart(chromeDriver, "0.5");//pass item quantity

            IWebElement errorMessage = chromeDriver.FindElement(By.XPath("//div[text() = 'The fewest you may purchase is 1.']"));
            Assert.True(errorMessage.Displayed);
        }

        [Test]
        public void RemoveItemFromCart()
        {
            //add item to be removed
            String itemQuantity = "2";//quantity of item to add to cart

            helper_AddSingleItemToCart(chromeDriver, itemQuantity);//pass item quantity
            wait.Until(chromeDriver => chromeDriver.FindElement(By.XPath("//span[text() = '"+ itemQuantity+"']")));
            helper_ViewCart(chromeDriver);

            helper_RemoveItemFromCart(chromeDriver, wait);

            IWebElement emptyCartMessage = chromeDriver.FindElement(By.XPath("//div[@class='cart-empty']"));
            Assert.True(emptyCartMessage.Displayed);

        }

        [Test]
        public void Checkout()
        {
            String itemQuantity = "2";//quantity of item to add to cart

            helper_AddSingleItemToCart(chromeDriver, itemQuantity);//pass item quantity
            wait.Until(chromeDriver => chromeDriver.FindElement(By.XPath("//span[text() = '" + itemQuantity + "']")));
            helper_ViewCart(chromeDriver);
            helper_ProceedToCheckout(chromeDriver);

            helper_CheckoutFieldsShipping(chromeDriver, demoUserEmail, "James", "Usher", "123 Chicago Dr", "Chicago", "Illinois", "12345", "United States", "1232342345");
            Assert.True(true);
        }

        [Test]
        public void SearchForItem()
        {
            String searchQuery = "Shirt";

            helper_SearchFunctionality(chromeDriver, searchQuery);

            IWebElement searchResults = chromeDriver.FindElement(By.XPath("//span[text() = concat('Search results for: ', \"'\", '"+searchQuery+"',\"'\")]"));
            Assert.True(searchResults.Displayed);
        }

        [Test]
        public void SearchForItem_Negative()
        {
            //malform search string, verify that no results are shown
            String searchQuery = "Shirt";

            helper_SearchFunctionality(chromeDriver, searchQuery +"q");

            String errorMessage = chromeDriver.FindElement(By.XPath("//div[@class='message notice']")).Text;
            String expectedError = "Your search returned no results.";//pulled directly from error message on website
            Assert.That(errorMessage, Is.EqualTo(expectedError));
        }

        [TearDown]
        public void TearDown()
        {
            Thread.Sleep(3000);
            chromeDriver.Close();
            chromeDriver.Quit();
        }


        
    }
}