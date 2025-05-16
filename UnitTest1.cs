using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI; 

namespace UnitTestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void TestUserCreation()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44377/User/Registration");
            driver.Manage().Window.Maximize();

            // Test Custemer Creation
            CreateCustomer(driver, "John", "Price", "price@gmail.com", "London, UK", "1111qqqq!");
            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("https://localhost:44377/User/Registration");
            // Test Employee Creation
            CreateEmployee(driver, "John", "Doe", "jdoe@gmail.com", "Seattle, USA", "qwer1234@");
        }



        [Test]
        public void TestUserLogin()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44377");
            driver.Manage().Window.Maximize();

            // Test with empty email and password
            driver.Navigate().GoToUrl("https://localhost:44377");
            PerformLogin(driver, "", "");
            Thread.Sleep(2000);

            // Test with empty password
            driver.Navigate().GoToUrl("https://localhost:44377");
            PerformLogin(driver, "muntasir.maruf26@gmail.com", "");
            Thread.Sleep(2000);

            // Test with empty email
            driver.Navigate().GoToUrl("https://localhost:44377");
            PerformLogin(driver, "", "1234");
            Thread.Sleep(2000);

            // Test with invalid email
            driver.Navigate().GoToUrl("https://localhost:44377");
            PerformLogin(driver, "muntasir.maruf6@gmail.com", "1234");
            Thread.Sleep(2000);

            // Test with invalid password
            driver.Navigate().GoToUrl("https://localhost:44377");
            PerformLogin(driver, "muntasir.maruf26@gmail.com", "12346");
            Thread.Sleep(2000);

            // Test with valid employee credentials
            driver.Navigate().GoToUrl("https://localhost:44377");
            PerformLogin(driver, "muntasir.maruf26@gmail.com", "1234");
            Thread.Sleep(2000);

            // Test with valid customer credentials
            driver.Navigate().GoToUrl("https://localhost:44377");
            PerformLogin(driver, "maruf.prottoy26@gmail.com", "123321");
            Thread.Sleep(2000);

            driver.Quit();
        }

        [Test]
        public void TestPlaceOrder()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44377");
            driver.Manage().Window.Maximize();

            PerformLogin(driver, "putin@gmail.com", "1234");
            PerformPlaceOrder(driver);
        }

        [Test]
        public void TestCancleOrder()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44377");
            driver.Manage().Window.Maximize();

            PerformLogin(driver, "putin@gmail.com", "1234");
            PerformCancelOrder(driver);
        }

        [Test]
        public void TestAddProduct()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44377");
            driver.Manage().Window.Maximize();

            PerformLogin(driver, "muntasir.maruf26@gmail.com", "1234");
            PerformAddProduct(driver, "New Product", "1", "5000", "This is a new product", "400");
        }

        [Test]
        public void TestAcceptOrder()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44377");
            driver.Manage().Window.Maximize();

            AcceptOrder(driver, "18");
        }

        private void AcceptOrder(IWebDriver driver, string orderId)
        {
            PerformLogin(driver, "muntasir.maruf26@gmail.com", "1234");

            driver.FindElement(By.Name("orders_btn")).Click();

            var acceptButton = driver.FindElement(By.Name("accept_btn_" + orderId));
            Thread.Sleep(1000);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", acceptButton);
            Thread.Sleep(1000);
            acceptButton.Click();
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", acceptButton);
        }



        private void CreateCustomer(IWebDriver driver, string fname, string lname, string email, string address, string password)
        {
            driver.FindElement(By.Name("FirstName")).SendKeys(fname);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("LastName")).SendKeys(lname);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("Email")).SendKeys(email);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("Address")).SendKeys(address);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("Password")).SendKeys(password);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("crate_btn")).Click();
        }


        private void CreateEmployee(IWebDriver driver, string fname, string lname, string email, string address, string password)
        {
            driver.FindElement(By.Name("FirstName")).SendKeys(fname);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("LastName")).SendKeys(lname);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("Email")).SendKeys(email);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("Address")).SendKeys(address);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("Password")).SendKeys(password);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("IsEmployee")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Name("crate_btn")).Click();
        }

        private void PerformLogin(IWebDriver driver, string emailStr, string passwordStr)
        {
            IWebElement email = driver.FindElement(By.Name("Email"));
            IWebElement password = driver.FindElement(By.Name("Password"));

            email.Clear();
            email.SendKeys(emailStr);

            password.Clear();
            password.SendKeys(passwordStr);
            Thread.Sleep(2000);

            driver.FindElement(By.Name("btn_login")).SendKeys(Keys.Return);
        }

        private void PerformPlaceOrder(IWebDriver driver)
        {
            Thread.Sleep(2000);
            driver.FindElement(By.Name("cart_btn")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("add_to_cart_btn_3")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("add_to_cart_btn_4")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Name("cart_btn")).Click();

            Thread.Sleep(2000);
            driver.FindElement(By.Name("place_order_btn")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Name("my_orders_btn")).Click();
        }

        public void PerformCancelOrder(IWebDriver driver)
        {
            Thread.Sleep(2000);
            driver.FindElement(By.Name("my_orders_btn")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.Name("details_btn_21")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Name("cancle_btn")).Click();
            Thread.Sleep(2000);
        }

        private void PerformAddProduct(IWebDriver driver, string productName, string productCategory, string productPrice, string productDescription, string productQuality)
        {
            Thread.Sleep(2000);
            driver.FindElement(By.Name("add_product_btn")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.Name("Name")).SendKeys(productName);
            Thread.Sleep(1000);
            SelectElement categoryDropdown = new SelectElement(driver.FindElement(By.Name("Category")));
            categoryDropdown.SelectByValue(productCategory);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("Description")).SendKeys(productDescription);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("Price")).SendKeys(productPrice);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("Quantity")).SendKeys(productQuality);
            Thread.Sleep(1000);

            driver.FindElement(By.Name("add_btn")).Click();
        }
    }
}