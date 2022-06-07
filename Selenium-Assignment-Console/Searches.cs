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
            driver.Navigate().GoToUrl(url); //go to google.co.nz
            driver.FindElement(By.Name("q")).SendKeys("Taupo weather"); //search for taupo weather
            driver.FindElement(By.Name("q")).SendKeys(Keys.Enter); // "Enter"

            string title = driver.Title; //get page title

            return title;
        }

        public string searchTradeMe(WebDriver driver)
        {
            string url = "http://www.trademe.co.nz/a/";
            driver.Navigate().GoToUrl(url); //go to trademe
            driver.FindElement(By.Id("search")).SendKeys("IT jobs"); //search for it jobs
            driver.FindElement(By.Id("search")).SendKeys(Keys.Enter); // "Enter"
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); //tells the driver to wait until the page loads
            wait.Until(ExpectedConditions.TitleIs("Jobs | Trade Me Jobs"));

            string title = driver.Title; //get page title

            return title; //return the page title
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
                    Console.WriteLine("Empty String. Continuing. \r\n"); //ignore empty string
                    continue;
                }
                else if (href.Contains("http"))
                {
                    Console.WriteLine(href + " Is a valid link");
                    IsValidLink(href); //validates link                    
                }
                else if (href.Contains("mailto"))
                {
                    Console.WriteLine("E-mail address detected"); //ignore email address
                    continue;
                }
                else
                {
                    Console.WriteLine(href + " is not a valid link"); //ignore invalid links
                    continue;
                }
            }          
        }

        public static bool IsValidLink(string href)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(href); //create a webrequest from the valid link sent by check links
                request.AllowAutoRedirect = true;

                if (href.Contains("http"))
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse(); //retrieves website response to indicate if a connection has been made
                    if (response.StatusCode == HttpStatusCode.OK) //checks the response
                    {
                        Console.WriteLine("\r\nResponse Status Code is: " + response.StatusCode + " \r\nStatus Description is: {0}", response.StatusDescription); //returns response code and description
                        response.Close();
                        return true; //returns true if a valid link is found
                    }
                    else
                    {
                        Console.WriteLine("\r\nLink is broken. Status Code is: " + response.StatusCode + " Status Description is: {0}", response.StatusDescription);
                        response.Close();
                        return false; //returns false if an error occurs
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid link" + "\r\nLink Address: " + href); 
                    return false;
                }
            }
            catch (WebException ex) //error handling
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
                if (num <= 7) //error handling
                {
                    var element = driver.FindElement(By.CssSelector(".ajax_add_to_cart_button[data-id-product='" + num + "']")); //add product to cart
                    element.Click(); //add item to cart

                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                    var element2 = driver.FindElement(By.ClassName("cross"));
                    element2.Click();//close popup window

                }else if (num <= 0 || num > 7) //error handling
                {
                    Console.WriteLine("Enter a number between 1 and 7");
                }
            }
            catch (Exception ex) //error handling
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
            var delete = driver.FindElement(By.XPath("//*[contains(@id, '" + num + "')][contains(@title, 'Delete')]")); //finds the delete element for the specific product
            driver.Navigate().GoToUrl(delete.GetAttribute("href")); //deletes the product          
        }

        public double getHighestPrice(WebDriver driver)
        {
            var itemPrices = driver.FindElements(By.XPath("//*[contains(@class, 'price')][contains(@id, 'total_product_price')]")); //finds the total product price of the product
            var shipping = driver.FindElement(By.XPath("//*[contains(@class, 'price')][contains(@id, 'total_shipping')]")).Text.Substring(1); //retrieves the shipping total
            var tax = driver.FindElement(By.XPath("//*[contains(@class, 'price')][contains(@id, 'total_tax')]")).Text.Substring(1); //retrieves tax total
            double maxprice = 0;
            double total = 0;
            int maxIndex = 0;

            for(int index = 0; index < itemPrices.Count; index++) //sets the index to start for the itemcount
            {
                if (Double.Parse(itemPrices[index].Text.Substring(1)) > maxprice) //finds the first price and index and compares to current maxprice
                {
                    maxprice = Double.Parse(itemPrices[index].Text.Substring(1)); //sets the maxprice if the itemPrice is higher
                    maxIndex = index; //sets the index for the maximum price set
                }
                Console.WriteLine("Adding price to total: " + itemPrices[index].Text.Substring(1));
                total += Double.Parse(itemPrices[index].Text.Substring(1)); //adds the itemprice to the total
            }

            var delete = driver.FindElements(By.XPath("//*[contains(@title, 'Delete')]")); //the delete function
            driver.Navigate().GoToUrl(delete[maxIndex].GetAttribute("href")); //finds the item in the maxprice index and deletes the product
            total -= maxprice; //remove the most expensive item price from the list
            total += Double.Parse(shipping); //add shipping
            total += Double.Parse(tax); //add tax
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Console.WriteLine("Most expensive item is: " + " $" + maxprice); //confirmation
            Console.WriteLine("Total: " + total); //confirmation

            return total; //return the total
        }

        public double getTotalPrice(WebDriver driver)
        {
            var price = driver.FindElement(By.Id("total_price")); //get the price
            double totalprice = 0;

            totalprice = Double.Parse(price.Text.Substring(1)); //parse element as a double

            return totalprice; //return price

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
                    continue; //ignore empty string
                }
                else if (href == null)
                {
                    continue; //ignore null addresses
                }
                else if (href.Contains("property"))
                {
                    Console.WriteLine("Property link found: " + href); //print any links with the string property
                    continue;
                }
                else if (href.Contains("services"))
                {
                    Console.WriteLine("Services link found: " + href); //print any links with the string property
                    continue;
                }
                else if (href.Contains("http"))
                {
                    continue; //ignore all other links
                }
                else
                {
                    continue; //ignore everything else
                }
            }
        }

        public void closeBrowser(WebDriver driver)
        {
            driver.Quit(); //closes the driver
        }
    }
}
