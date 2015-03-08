using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SimpleLogging.Core;
using Nancy;
using Nancy.Testing;
using HipsterTechnologies.API.Models;
using HipsterTechnologies.API.Models.Contexts;
using HipsterTechnologies.API.Routes.Modules;
using HipsterTechnologies.API.Services.MarkIt;
using System.Data.Entity;
using Nancy.Responses.Negotiation;

namespace HipsterTechnologies.API.Routes.Tests.Modules
{
    /// <summary>
    /// Unit tests for the Transaction Module.
    /// </summary>
    [TestFixture]
    public class TransactionModuleTests
    {
        #region Mocks

        public Mock<ILoggingService> LoggerMock { get; set; }

        public Mock<DbSet<Transaction>> TransactionDbSetMock { get; set; }

        public Mock<ModelContext> ModelContextMock { get; set; }

        public Mock<IModelContextFactory> ModelContextFactoryMock { get; set; }

        public Mock<IStockMarketService> StockMarketServiceMock { get; set; }

        public Browser Browser { get; set; }

        #endregion

        #region Data/Stubs

        public IEnumerable<TransactionItem> SampleTransactionItems
        {
            get
            {
                var transactionItems = new[] {
                    new TransactionItem { Exchange = "NASDAQ", Symbol = "APPL", PostedPrice = 123.45m, Quantity = 2, Type = TransactionType.Purchase },
                    new TransactionItem { Exchange = "NASDAQ", Symbol = "GOOG", PostedPrice = 123.45m, Quantity = 2, Type = TransactionType.Purchase },
                    new TransactionItem { Exchange = "NASDAQ", Symbol = "TSLA", PostedPrice = 123.45m, Quantity = 2, Type = TransactionType.Purchase },
                    new TransactionItem { Exchange = "NASDAQ", Symbol = "AMZN", PostedPrice = 123.45m, Quantity = 2, Type = TransactionType.Purchase },
                    new TransactionItem { Exchange = "NASDAQ", Symbol = "APPL", PostedPrice = 123.45m, Quantity = 2, Type = TransactionType.Sale },
                    new TransactionItem { Exchange = "NASDAQ", Symbol = "GOOG", PostedPrice = 123.45m, Quantity = 2, Type = TransactionType.Sale },
                    new TransactionItem { Exchange = "NASDAQ", Symbol = "TSLA", PostedPrice = 123.45m, Quantity = 2, Type = TransactionType.Sale },
                    new TransactionItem { Exchange = "NASDAQ", Symbol = "AMZN", PostedPrice = 123.45m, Quantity = 2, Type = TransactionType.Sale }
                };
                return new List<TransactionItem>(transactionItems);
            }
        }

        public IEnumerable<TransactionItem> SampleSaleTransactionItems
        {
            get { return SampleTransactionItems.Where(t => t.Type == TransactionType.Sale); }
        }

        public IEnumerable<TransactionItem> SamplePurchaseTransactionItems
        {
            get { return SampleTransactionItems.Where(t => t.Type == TransactionType.Purchase); }
        }

        public Transaction SampleTransaction
        {
            get
            {
                var transaction = new Transaction
                {
                    TransactionId = 1337,
                    FacebookHandle = "morgan.cabral",
                    PostedTime = DateTime.Now,
                    TransactionItems = new List<TransactionItem>(SampleTransactionItems)
                };
                return transaction;
            }
        }

        public Transaction SampleSaleTransaction
        {
            get
            {
                var transaction = new Transaction
                {
                    FacebookHandle = "morgan.cabral",
                    PostedTime = DateTime.Now,
                    TransactionItems = new List<TransactionItem>(SampleSaleTransactionItems)
                };
                return transaction;
            }
        }

        public Transaction SamplePurchaseTransaction
        {
            get
            {
                var transaction = new Transaction
                {
                    FacebookHandle = "morgan.cabral",
                    PostedTime = DateTime.Now,
                    TransactionItems = new List<TransactionItem>(SamplePurchaseTransactionItems)
                };
                return transaction;
            }
        }

        #endregion

        [SetUp]
        public void SetUp()
        {
            // Setup a mock for the logging service.
            LoggerMock = new Mock<ILoggingService>(MockBehavior.Loose);

            // Setup a mock for anything that has to do with the database.
            TransactionDbSetMock = new Mock<DbSet<Transaction>>(MockBehavior.Loose);
            TransactionDbSetMock.Setup(db => db.Add(It.IsAny<Transaction>())).Returns(SampleTransaction);

            ModelContextMock = new Mock<ModelContext>(MockBehavior.Loose);
            ModelContextMock.Setup(context => context.Transactions).Returns(TransactionDbSetMock.Object);

            ModelContextFactoryMock = new Mock<IModelContextFactory>(MockBehavior.Loose);
            ModelContextFactoryMock.Setup(factory => factory.CreateModelContext()).Returns(ModelContextMock.Object);

            // Setup a mock for the Stock Market service.
            StockMarketServiceMock = new Mock<IStockMarketService>(MockBehavior.Loose);
            StockMarketServiceMock.Setup(mock => mock
                .Quote(It.IsAny<String>()))
                // Returns a completed task. Moq chokes on anything else.
                .Returns(Task.FromResult(new Stock())); 

            // Setup the test browser object.
            Browser = new Browser(cfg => {
                cfg.Module<TransactionModule>();
                cfg.ResponseProcessors(typeof(JsonProcessor));
                cfg.Dependency<ILoggingService>(LoggerMock.Object);
                cfg.Dependency<IModelContextFactory>(ModelContextFactoryMock.Object);
                cfg.Dependency<IStockMarketService>(StockMarketServiceMock.Object);
            });
        }

        [Test]
        public void PostTransactionsLocationHeaderTest()
        {
            // Exercise the module.
            var response = Browser.Post("/transactions", with => {
                with.JsonBody<Transaction>(SampleTransaction);
            });

            // Verify state.
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual("/transactions/1337", response.Headers["Location"]);
        }

        [Test]
        public void PostTransactionReturns201StatusCodeOnSuccessTest()
        {
            // Exercise the module.
            var response = Browser.Post("/transactions", with => {
                with.JsonBody<Transaction>(SampleTransaction);
            });

            // Verify state.
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public void PostTransactionsStoresTransactionTest()
        {
            // Exercise the module.
            var response = Browser.Post("/transactions", with => {
                with.JsonBody<Transaction>(SampleTransaction);
            });

            // Verify behavior.
            TransactionDbSetMock.Verify(db => db.Add(It.IsAny<Transaction>()), Times.Once);
            ModelContextMock.Verify(context => context.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void PostTransactionsUsesStockMarketService()
        {
            // Exercise the module.
            var response = Browser.Post("/transactions", with => {
                with.JsonBody<Transaction>(SampleTransaction);
            });

            // Verify behavior.
            StockMarketServiceMock.Verify(service => service.Quote("APPL"), Times.Exactly(2));
            StockMarketServiceMock.Verify(service => service.Quote("GOOG"), Times.Exactly(2));
            StockMarketServiceMock.Verify(service => service.Quote("TSLA"), Times.Exactly(2));
            StockMarketServiceMock.Verify(service => service.Quote("AMZN"), Times.Exactly(2));
        }
    }
}
