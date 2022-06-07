using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium_Assignment_Console;
using System;

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
            search.closeBrowser(driver);
            
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
        public void CheckAndVerifyLinks()
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
            
            Assert.AreEqual(57.98, result);
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

            Assert.AreEqual(76.01, Math.Round(result, 2));

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
