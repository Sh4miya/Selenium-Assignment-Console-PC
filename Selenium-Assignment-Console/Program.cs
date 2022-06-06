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

           string url1 = "http://www.google.co.nz";

           search.searchGoogle(driver);


           //string url2 = "http://www.trademe.co.nz/a/";
           //search.searchTradeMe(url2);

            string url3 = "http://automationpractice.com";
           // search.TestLinks(driver);
           //driver.Navigate().GoToUrl(url1); //go to automation practice


            //search.addToCart(driver, 1);


        }



    }
}
