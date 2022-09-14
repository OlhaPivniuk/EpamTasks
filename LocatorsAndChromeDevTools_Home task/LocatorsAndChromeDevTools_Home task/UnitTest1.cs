using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace LocatorsAndChromeDevTools_Home_task
{    
        [TestFixture]
        public class Class1
        {
            private IWebDriver driver;

            [SetUp]
            public void SetUpDriver()
            {
                driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://accounts.ukr.net/login");
                driver.Manage().Window.Maximize();
            }

        [TestCase("pivniuktest@ukr.net", "This is simple mail", "лист")]
        public void SendMail(string email, string textMail, string expectedMessage)
        {
                       
            driver.FindElement(By.Name("login")).SendKeys("testepampivniuk@ukr.net");
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("PassTestEpam");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementIsVisible(By.ClassName("login-button__user")));
            
            driver.FindElement(By.XPath("//button[starts-with(@class,'button')]")).Click();                       
            driver.FindElement(By.Name("toFieldInput")).SendKeys(email);
            driver.FindElement(By.CssSelector("input[name$='subject']")).SendKeys(textMail);
            driver.FindElement(By.XPath("//button[@class='button primary send']")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementIsVisible(By.ClassName("link3")));
            var actualMessage = driver.FindElement(By.LinkText ("лист")).Text;
            Assert.AreEqual(expectedMessage, actualMessage);
        }
        [TearDown]
        public void CloseAndQuitDriver()
        {
            driver.Close();
            driver.Quit();
        }
    }
}