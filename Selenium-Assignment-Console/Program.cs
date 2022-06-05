using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Selenium_Assignment_Console
{
    public class Program
    {
        static void Main(string[] args)
        {
           var search1 = new Searches();
           //string url1 = "http://www.google.co.nz";
           //search1.searchGoogle(url1);

           //var search2 = new Searches();
           //string url2 = "http://www.trademe.co.nz/a/";
           //search2.searchTradeMe(url2);

            var search3 = new Searches();
            string url3 = "http://automationpractice.com";
            search3.TestAndVerifyLinks(url3);

            //var search4 = new Searches();
            //search4.addToCart(url3);


        }



    }
}
