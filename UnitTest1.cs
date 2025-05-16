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
        public void TestUerCreation()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44377/User/Registration");
            driver.Manage().Window.Maximize();

            IWebElement fname = driver.FindElement(By.Name("FirstName"));
            fname.SendKeys("Vladimir");
            Thread.Sleep(1000);

            IWebElement lname = driver.FindElement(By.Name("LastName"));
            lname.SendKeys("Putin");
            Thread.Sleep(1000);

            IWebElement email = driver.FindElement(By.Name("Email"));
            email.SendKeys("putin@gmail.com");
            Thread.Sleep(1000);

            IWebElement address = driver.FindElement(By.Name("Address"));
            address.SendKeys("Moscow");
            Thread.Sleep(1000);

            IWebElement password = driver.FindElement(By.Name("Password"));
            password.SendKeys("1234");
            Thread.Sleep(1000);

            IWebElement register_btn = driver.FindElement(By.Name("crate_btn"));
            register_btn.Click();

            driver.Navigate().GoToUrl("https://localhost:44377");

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

            PerformLogin(driver, "maruf.prottoy26@gmail.com", "123321");
            PerformPlaceOrder(driver);

            driver.Navigate().GoToUrl("https://localhost:44377/Order/Home");
 
        }

        [Test]
        public void TestCancleOrder()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44377");
            driver.Manage().Window.Maximize();

            PerformLogin(driver, "maruf.prottoy26@gmail.com", "123321");
            PerformCancelOrder(driver);

            driver.Navigate().GoToUrl("https://localhost:44377/Order/Home");
        }

        [Test]
        public void TestAddProduct()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44377");
            driver.Manage().Window.Maximize();

            PerformLogin(driver, "muntasir.maruf26@gmail.com", "1234");
            PerformAddProduct(driver);
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
        }

        public void PerformCancelOrder(IWebDriver driver)
        {
            Thread.Sleep(2000);
            driver.FindElement(By.Name("my_orders_btn")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.Name("details_btn_14")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Name("cancle_btn")).Click();
            Thread.Sleep(2000);
        }

        private void PerformAddProduct(IWebDriver driver)
        {
            Thread.Sleep(2000);
            driver.FindElement(By.Name("add_product_btn")).Click();
            Thread.Sleep(2000);
            IWebElement productName = driver.FindElement(By.Name("Name"));
            IWebElement productCategory = driver.FindElement(By.Name("Category"));
            IWebElement productPrice = driver.FindElement(By.Name("Price"));
            IWebElement productDescription = driver.FindElement(By.Name("Description"));
            IWebElement productQuantity = driver.FindElement(By.Name("Quantity"));

            productName.SendKeys("Test Product 1");
            Thread.Sleep(1000);
            SelectElement categoryDropdown = new SelectElement(productCategory);
            categoryDropdown.SelectByValue("2");
            Thread.Sleep(1000);
            productDescription.SendKeys("This is a test product 2.");
            Thread.Sleep(1000);
            productPrice.SendKeys("100");
            Thread.Sleep(1000);
            productQuantity.SendKeys("500");
            Thread.Sleep(1000);

            driver.FindElement(By.Name("add_btn")).Click();
        }

    }
}