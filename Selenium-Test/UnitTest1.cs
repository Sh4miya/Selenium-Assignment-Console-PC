using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium_Assignment_Console;


namespace Selenium_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var search = new Searches();
            string url = "http://www.google.co.nz";
            var result = search.searchGoogle(url);

            Assert.AreEqual(result, "Taupo weather - Google Search");
            
        }

        [TestMethod]
        public void TestMethod2()
        {
            var search = new Searches();
            string url = "http://www.trademe.co.nz/a/";
            var result = search.searchTradeMe(url);

            Assert.AreEqual(result, "Jobs | Trade Me Jobs");

        }

        [TestMethod]
        public void TestMethod3()
        {
            var search = new Searches();
            string url = "http://automationpractice.com";
            var result = search.addToCart(url);

            Assert.AreEqual(result, "$55.98");
        }
    }
}
