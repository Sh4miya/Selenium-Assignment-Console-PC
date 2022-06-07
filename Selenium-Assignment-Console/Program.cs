using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Selenium_Assignment_Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            WebDriver driver = new ChromeDriver(); //create a chrome driver
            var search = new Searches();
            string url = "http://automationpractice.com";
            driver.Navigate().GoToUrl(url); //go to automation practice
            //search.searchGoogle(driver);
            //search.searchTradeMe(driver);
            //search.TestLinks(driver);
            search.addToCart(driver, 1);
            search.addToCart(driver, 2);
            search.addToCart(driver, 4);
            search.addToCart(driver, 6);
            search.goToCheckout(driver);
            //search.removeFromCart(driver, 1);
            search.getHighestPrice(driver);
        }



    }
}
