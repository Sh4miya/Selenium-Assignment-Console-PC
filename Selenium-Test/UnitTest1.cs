using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium_Assignment_Console;


namespace Selenium_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SearchTaupoWeather()
        {
            var search = new Searches();
            WebDriver driver = new ChromeDriver(); //create a chrome driver
            var result = search.searchGoogle(driver);

            Assert.AreEqual(result, "Taupo weather - Google Search");
            
        }

        [TestMethod]
        public void FindTradeMeJobs()
        {
            var search = new Searches();
            WebDriver driver = new ChromeDriver(); //create a chrome driver
            var result = search.searchTradeMe(driver);
            search.closeBrowser(driver);

            Assert.AreEqual(result, "Jobs | Trade Me Jobs");

        }

        [TestMethod]
        public void CheckLinks()
        {
            var search = new Searches();
            WebDriver driver = new ChromeDriver(); //create a chrome driver
            search.TestLinks(driver);
            search.closeBrowser(driver);
        }

        [TestMethod]
        public void AddThreeItemsRemoveOne()
        {
            var search = new Searches();
            string url = "http://automationpractice.com";
            WebDriver driver = new ChromeDriver(); //create a chrome driver
            driver.Navigate().GoToUrl(url); //go to automation practice

            search.addToCart(driver, 1);
            search.addToCart(driver, 2);
            search.addToCart(driver, 5);
            search.goToCheckout(driver);
            search.removeFromCart(driver, 1);
            double result = search.getTotalPrice(driver);

            search.closeBrowser(driver);
            
            Assert.AreEqual(result, 57.98);
        }

        [TestMethod]
        public void AddFourItemsRemoveExpensiveOne()
        {
            var search = new Searches();
            string url = "http://automationpractice.com";
            WebDriver driver = new ChromeDriver(); //create a chrome driver
            driver.Navigate().GoToUrl(url); //go to automation practice

            search.addToCart(driver, 1);
            search.addToCart(driver, 2);
            search.addToCart(driver, 4);
            search.addToCart(driver, 6);
            search.goToCheckout(driver);

            double result = search.getHighestPrice(driver);

            search.closeBrowser(driver);

            Assert.AreEqual(result, 94.50);

        }

        [TestMethod]
        public void TestTradeMeLinks()
        {
            var search = new Searches();
            WebDriver driver = new ChromeDriver(); //create a chrome driver
            search.allTradeMeLinks(driver);

            search.closeBrowser(driver);
        }
    }
}
