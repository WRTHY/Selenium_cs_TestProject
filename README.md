# Selenium_cs_TestProject
Automation project pointed at a dummy storefront (https://magento.softwaretestingboard.com/). Written in Selenium and C#

In order to get started, open this repository in the IDE of your choice and double click the Selenium_TestProject_cs.sln file. 
Once the workspace has been recognized as a selenium project, navigate to the "TestSuite" file and run the test cases individually, or as a group

---Desc---

This project implements Page Object Model Selenium design pattern with NUnit framework test suite. This suite pulls from several helper methods found in the HelperMethods file to make code more readable and to keep the TestSuite file more concise. The bullet point under each message describes my approach and the element that I am validating against:

-	CreateUser() 
  o	Creates new user, validates “My Account” banner is displayed
-	CreateUser_Negative()
  o	Passes empty email string, validates error message is displayed
-	LoginWithUsernameAndPassword()
  o	Logs into website using demo email/password provided by site, validates captcha showing (more information below)
-	LoginWithUsernameAndPassword_Negative()
  o	Malforms email by adding character to the end of the demo email, validates the captcha (more information below)
-	AddItemToCart()
  o	Selects first item on homepage, adds a quantity of that item to the user’s cart. Validates the value displayed next to the cart is equal to the input item quantity
-	AddItemToCart_Negative()
  o	Attempts to add an item to the cart with a quantity less than one. Validates text error message displayed telling user to add at least one item
-	RemoveItemFromCart() – currently failing arbitrarily due to timing issue
  o	Adds item to cart then navigates to cart and clicks the trash can icon in the bottom right related to the newly added item. Validates the cart empty message. This test case is currently failing when executed in conjunction with ither
-	Checkout() – currently failing
  o	Unable to send keys to email box on first page of checkout, this case is incomplete. Currently passing true in the validation, case is failing on sendkeys to email field
-	SearchForItem()
  o	Passed in search query (‘Shirt’ in this instance) and hit enter. Validates against “Search results for ‘query’” message at top of page
-	SearchForItem_Negative()
  o	Malform search query by adding extra character to the end. Validates against the “Your search returned no results” error message that is displayed to the user

Notable issues I encountered while creating the suite:
-	Timing: Elements such as the items in cart counter and buttons on several of the pages required time to load initially causing a headache with some testing. ImplicitWait and ExplicitWait calls mitigated some of these issues, but I experienced inconsistent behavior using both functions
-	Login Captcha: The Magento website detected the presence of my automation and required a captcha, blocking testing of login functionality. Current login test cases verify presence of the Captcha (appears once login button is pressed) instead of on-screen content displayed upon successful login
-	Apostrophes in Xpath locaters: Validation for the search functions is done against an element that appears on the page with apostrophes in its plaintext. Utilized concat method in xPath call to correctly search for text containing apostrophes
-	Page Object Model: Not really an issue, but I had never worked with the Page Object Model design pattern in selenium before this project. The only helper method that uses the homePage/loginPage is the helper_LoginSteps, which is used in the LoginWithUsernameAndPassword() test case and its negative.

As a closing note, I did not create a negative test case for Checkout or RemoveItemFromCart due to timing issues encountered during test process
