using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using HipsterTechnologies.API.Services.MarkIt;


namespace HipsterTechnologies.API.Services.MarkIt.Tests
{
    [TestFixture]
    public class MarkItServiceTest
    {
        public IStockMarketService Service { get; set; }

        [SetUp]
        public void SetUp()
        {
            Service = new MarkItService();
        }

        /// <summary>
        /// NOTE: Shitty unit test is shitty. 
        /// TODO: Make less shitty.
        /// </summary>
        [Test]
        public async void LookupGenericWordTest()
        {
            var stocks = await Service.Lookup("Lab");
            Assert.IsNotEmpty(stocks);
        }

        /// <summary>
        /// NOTE: Shitty unit test is shitty. 
        /// TODO: Make less shitty.
        /// </summary>
        [Test]
        public async void LookupGibberishTest()
        {
            var stocks = await Service.Lookup("ThisIsDefinatelyInvalid!@#$");
            Assert.IsEmpty(stocks);
        }

        /// <summary>
        /// NOTE: Shitty unit test is shitty. 
        /// TODO: Make less shitty.
        /// </summary>
        [Test]
        public async void QuoteAppleTest()
        {
            var stock = await Service.Quote("AAPL");
            Assert.IsTrue(stock.Name.Contains("Apple"));
        }
    }
}
