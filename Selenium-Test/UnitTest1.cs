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

            Assert.AreEqual(result, "Jobs | Trade Me Jobs");

        }

        [TestMethod]
        public void CheckLinks()
        {
            var search = new Searches();
            WebDriver driver = new ChromeDriver(); //create a chrome driver
            search.TestLinks(driver);
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
            string result = search.getTotalPrice(driver);
            
            Assert.AreEqual(result, "$57.98");
        }
    }
}
