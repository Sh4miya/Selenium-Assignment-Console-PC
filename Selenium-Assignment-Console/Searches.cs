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
using System.Text.RegularExpressions;

namespace Selenium_Assignment_Console
{
    public class Searches
    {
        public string searchGoogle(WebDriver driver)
        {
            string url = "http://www.google.co.nz";
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.Name("q")).SendKeys("Taupo weather"); //search for taupo weather
            driver.FindElement(By.Name("q")).SendKeys(Keys.Enter); // "Enter"

            string title = driver.Title;

            closeBrowser(driver);
            return title;
        }

        public string searchTradeMe(WebDriver driver)
        {
            string url = "http://www.trademe.co.nz/a/";
            driver.Navigate().GoToUrl(url); //go to trademe
            driver.FindElement(By.Id("search")).SendKeys("IT jobs"); //search for it jobs
            driver.FindElement(By.Id("search")).SendKeys(Keys.Enter); // "Enter"
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.TitleIs("Jobs | Trade Me Jobs"));

            string title = driver.Title;

            closeBrowser(driver);

            return title;

        }

        public void TestLinks(WebDriver driver)
        {
            string url = "http://automationpractice.com";
            driver.Navigate().GoToUrl(url); //go to automation practice
            ReadOnlyCollection<IWebElement> links = driver.FindElements(By.TagName("a")); //get all the links

            foreach (IWebElement link in links)
            {
                string href = link.GetAttribute("href");
                if (href == "")
                {
                    Console.WriteLine("Empty String. Continuing. \r\n");
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

        public void addToCart(WebDriver driver, int num)
        {
            try
            {
                if (num <= 7)
                {
                    int item;
                    item = num;

                    var element = driver.FindElement(By.CssSelector(".ajax_add_to_cart_button[data-id-product='" + item + "']")); //add product
                    element.Click(); //add item to cart

                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                    var element2 = driver.FindElement(By.ClassName("cross"));
                    element2.Click();//close popup window
                }else if (num <= 0 || num > 7)
                {
                    Console.WriteLine("Enter a number between 1 and 7");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void goToCheckout(WebDriver driver)
        {
            var element = driver.FindElement(By.CssSelector("a.btn.btn-default.button.button-medium[title='Proceed to checkout']")); //go to checkout
            element.Click(); //proceed to checkout
        }

        public void removeFromCart(WebDriver driver, int num)
        {
            var delete = driver.FindElement(By.XPath("//*[contains(@id, '" + num + "')][contains(@title, 'Delete')]"));
            driver.Navigate().GoToUrl(delete.GetAttribute("href"));          
        }

        public double getHighestPrice(WebDriver driver)
        {
            var itemPrices = driver.FindElements(By.XPath("//*[contains(@class, 'price')][contains(@id, 'total_product_price')]"));
            double maxprice = 0;
            double total = 0;
            int maxIndex = 0;
            var delete = driver.FindElements(By.XPath("//*[contains(@title, 'Delete')]"));

            for(int index = 0; index < itemPrices.Count; index++)
            {
                if (Double.Parse(itemPrices[index].Text.Substring(1)) > maxprice)
                {
                    maxprice = Double.Parse(itemPrices[index].Text.Substring(1));
                    total += maxprice;
                    maxIndex = index;
                }
            }

            Console.WriteLine("Most expensive item is: " + " $" + maxprice);

            driver.Navigate().GoToUrl(delete[maxIndex].GetAttribute("href"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            return total;
        }

        public double getTotalPrice(WebDriver driver)
        {
            var price = driver.FindElement(By.Id("total_price")); //get the price
            double totalprice = 0;

            totalprice = Double.Parse(price.Text.Substring(1));
            closeBrowser(driver);

            return totalprice;

        }

        public void allTradeMeLinks(WebDriver driver)
        {
            string url = "http://www.trademe.co.nz/a/";
            driver.Navigate().GoToUrl(url); //go to trademe

            ReadOnlyCollection<IWebElement> links = driver.FindElements(By.TagName("a")); //get all the links

            foreach (IWebElement link in links)
            {
                string href = link.GetAttribute("href");
                if (href == "")
                {
                    continue;
                }
                else if (href == null)
                {
                    continue;
                }
                else if (href.Contains("property"))
                {
                    Console.WriteLine("Property link found: " + href);
                    continue;
                }
                else if (href.Contains("services"))
                {
                    Console.WriteLine("Services link found: " + href);
                    continue;
                }
                else if (href.Contains("http"))
                {
                    continue;
                }
                else
                {
                    continue;
                }
            }
        }

        public void closeBrowser(WebDriver driver)
        {
            driver.Quit();
        }
    }
}
