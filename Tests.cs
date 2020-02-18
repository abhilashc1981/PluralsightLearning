using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AutomationPractice
{
    class Tests
    {
        IWebDriver ChromeDriver { get; set; }

        [SetUp]
        public void StartBrowser()
        {
            ChromeDriver = new OpenQA.Selenium.Chrome.ChromeDriver(DependencyInjecter.Driver.ChromeDriverPath);
        }

        void NavigateToAutomationPracticeHomePage()
        {
            ChromeDriver.Url = "http://automationpractice.com/index.php";
            ChromeDriver.Manage().Window.Maximize();
        }

        [Test]
        public void VerifySearch()
        {
            NavigateToAutomationPracticeHomePage();
            IWebElement searchField = FindElementById("search_query_top");
            string searchTerm = "dress";
            searchField.SendKeys(searchTerm);
            IWebElement searchButton = ChromeDriver.FindElement(By.Name("submit_search"));
            searchButton.Click();
            IWebElement searchStringSpan = FindElementByClassName("lighter");
            Assert.AreEqual(searchTerm.ToUpper(), searchStringSpan.Text.ToUpper().Replace('"', ' ').Trim());
            IWebElement resultCounterSpan = FindElementByClassName("heading-counter");
            int numberOfResults = 0;
            string numberOfResultsText = resultCounterSpan.Text.Split(' ')[0];
            int.TryParse(numberOfResultsText, out numberOfResults);
            IWebElement paginationDiv = FindElementByClassName("product-count");
            int numberOfResultsInPagination = 0;
            string numberOfResultsInPaginationText = paginationDiv.Text.Split(' ')[5];
            int.TryParse(numberOfResultsInPaginationText, out numberOfResultsInPagination);
            Assert.AreEqual(numberOfResults, numberOfResultsInPagination);
        }

        IWebElement FindElementByClassName(string className)
        {
            return ChromeDriver.FindElement(By.ClassName(className));
        }

        [Test]
        public void VerifyUserRegistration()
        {
            NavigateToAutomationPracticeHomePage();
            IWebElement signInLink = FindElementByClassName("login");
            signInLink.Click();
            IWebElement emailField = FindElementById("email_create");
            string emailId = System.DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "") + "@gmail.com";
            emailField.SendKeys(emailId);
            IWebElement createAnAccountButton = FindElementById("SubmitCreate");
            createAnAccountButton.Click();
            System.Threading.Thread.Sleep(2000);
            IWebElement firstNameField = FindElementById("customer_firstname");
            string firstName = "Abhilash";
            firstNameField.SendKeys(firstName);
            IWebElement lastNameField = FindElementById("customer_lastname");
            string lastName = "Chandrashekar";
            lastNameField.SendKeys(lastName);
            IWebElement passwordField = FindElementById("passwd");
            passwordField.SendKeys("password");
            IWebElement addressFirstNameField = FindElementById("firstname");
            addressFirstNameField.SendKeys(firstName);
            IWebElement addressLastNameField = FindElementById("lastname");
            addressLastNameField.SendKeys(lastName);
            IWebElement addressField = FindElementById("address1");
            addressField.SendKeys("714, Vine Street");
            IWebElement cityField = FindElementById("city");
            cityField.SendKeys("Anaheim");
            IWebElement stateDropDown = FindElementById("id_state");
            stateDropDown.Click();
            var state = new SelectElement(stateDropDown);
            state.SelectByText("California");
            IWebElement zipCodeField = FindElementById("postcode");
            zipCodeField.SendKeys("90045");
            IWebElement mobilePhoneField = FindElementById("phone_mobile");
            mobilePhoneField.SendKeys("+11234567890");
            IWebElement registerButton = FindElementById("submitAccount");
            registerButton.Click();
            IWebElement userNameLink = FindElementByClassName("account");
            Assert.AreEqual(firstName + " " + lastName, userNameLink.Text);
            IWebElement signOutLink = FindElementByClassName("logout");
            Assert.IsTrue(signOutLink.Displayed);
        }

        IWebElement FindElementById(string id)
        {
            return ChromeDriver.FindElement(By.Id(id));
        }

        [TearDown]
        public void CloseBrowser()
        {
            System.Threading.Thread.Sleep(3000);
            ChromeDriver.Quit();
        }

        private DependencyInjecter dependencyInjecter;
        public DependencyInjecter DependencyInjecter
        {
            get
            {
                if (dependencyInjecter == null)
                {
                    dependencyInjecter = new DependencyInjecter();
                }
                return dependencyInjecter;
            }
        }
    }
}
