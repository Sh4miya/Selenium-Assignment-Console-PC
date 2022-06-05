using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using HtmlAgilityPack;


namespace Selenium_Assignment_Console
{
    public class Searches
    {
        public string searchGoogle(string url)
        {
            WebDriver driver = new ChromeDriver(); //create a chrome driver
            driver.Navigate().GoToUrl(url); //go to google.co.nz
            driver.FindElement(By.Name("q")).SendKeys("Taupo weather"); //search for taupo weather
            driver.FindElement(By.Name("q")).SendKeys(Keys.Enter); // "Enter"

            string title = driver.Title;

            closeBrowser(driver);
            return title;
        }

        public string searchTradeMe(string url)
        {
            
            WebDriver driver = new ChromeDriver(); //create a chrome driver

            driver.Navigate().GoToUrl(url); //go to trademe
            driver.FindElement(By.Id("search")).SendKeys("IT jobs"); //search for taupo weather
            driver.FindElement(By.Id("search")).SendKeys(Keys.Enter); // "Enter"
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.TitleIs("Jobs | Trade Me Jobs"));

            string title = driver.Title;

            closeBrowser(driver);

            return title;

        }

        public void TestAndVerifyLinks(string url)
        {
            WebDriver driver = new ChromeDriver(); //create a chrome driver
            driver.Navigate().GoToUrl(url); //go to automation practice
            ReadOnlyCollection<IWebElement> links = driver.FindElements(By.TagName("a")); //get all the links

            foreach (IWebElement link in links)
            {
                string href = link.GetAttribute("href");
                if (href == "")
                {
                    Console.WriteLine("Empty String");
                    continue;
                }
                else if (href.Contains("http"))
                {
                    Console.WriteLine(href + " Is a valid link");
                    IsValidLink(href);                    
                }
                else if (href.Contains("mailto"))
                {
                    Console.WriteLine("E-mail address detected");
                    continue;
                }
                else
                {
                    Console.WriteLine(href + " is not a valid link");
                    continue;
                }
            }
            closeBrowser(driver);
        }

        public static bool IsValidLink(string href)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(href);
                request.AllowAutoRedirect = true;

                if (href.Contains("http"))
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("\r\nResponse Status Code is: " + response.StatusCode + " \r\nStatus Description is: {0}", response.StatusDescription);
                        response.Close();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("\r\nLink is broken. Status Code is: " + response.StatusCode + " Status Description is: {0}", response.StatusDescription);
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid link" + "\r\nLink Address: " + href);
                    return false;
                }
            }
            catch (WebException ex)
            {
                var errorReponse = (HttpWebResponse)ex.Response;
                Console.WriteLine("Status is: " + errorReponse);
                return false;
            }

        }

        public string addToCart(string url)
        {

            WebDriver driver = new ChromeDriver(); //create a chrome driver
            driver.Navigate().GoToUrl(url); //go to automation practice

            var element = driver.FindElement(By.CssSelector(".ajax_add_to_cart_button[data-id-product='1']")); //add product
            element.Click(); //add item to cart

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var element2 = driver.FindElement(By.ClassName("cross")); 
            element2.Click();//close popup window

            var element3 = driver.FindElement(By.CssSelector(".ajax_add_to_cart_button[data-id-product='2']")); //add product
            element3.Click(); //add item to cart

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var element4 = driver.FindElement(By.ClassName("cross"));
            element4.Click(); //close popup window

            driver.FindElement(By.Id("search_query_top")).Clear();

            var element5 = driver.FindElement(By.CssSelector(".ajax_add_to_cart_button[data-id-product='5']"));
            element5.Click(); //add item to cart

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var element6 = driver.FindElement(By.CssSelector("a.btn.btn-default.button.button-medium[title='Proceed to checkout']")); //go to checkout
            element6.Click(); //close popup window

            var element7 = driver.FindElement(By.Id("1_1_0_0"));
            element7.Click(); //removes first item from the cart

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("1_1_0_0"))); //waits until the item has been removed

            var price = driver.FindElement(By.Id("total_price")); //get the price

            string priceString = price.Text; //convert price to string

            closeBrowser(driver);

            return priceString;

        }

        public void closeBrowser(WebDriver driver)
        {
            driver.Quit();
        }
    }
}
