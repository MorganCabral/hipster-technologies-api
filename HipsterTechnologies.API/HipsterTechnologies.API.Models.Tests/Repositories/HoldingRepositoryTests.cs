using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using HipsterTechnologies.API.Models.Contexts;
using System.Data.Entity;
using HipsterTechnologies.API.Models.Repositories;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;

namespace HipsterTechnologies.API.Models.Tests.Repositories
{
    [TestFixture]
    public class HoldingRepositoryTest
    {
        /// <summary>
        /// Mock object for the ModelContextFactory class that is passed into
        /// the Repostory we are testing.
        /// </summary>
        public Mock<IModelContextFactory> ModelFactoryMock { get; set; }

        /// <summary>
        /// Test mock for the ModelContext that gets created by
        /// the ModelFactoryMock.
        /// </summary>
        public Mock<ModelContext> ModelContextMock { get; set; }

        /// <summary>
        /// Test mock for the Holdings collection in ModelContextMock.
        /// </summary>
        public Mock<IDbSet<Holding>> HoldingDbSetMock { get; set; }

        /// <summary>
        /// Test data which can optionally be returned by the HoldingDbSetMock
        /// when we need to test that sort of thing.
        /// </summary>
        public IQueryable<Holding> HoldingTestData { get; set; }

        [SetUp]
        public void SetUp()
        {
            HoldingTestData = new List<Holding>().AsQueryable();
            HoldingDbSetMock = new Mock<IDbSet<Holding>>();

            ModelContextMock = new Mock<ModelContext>();
            ModelContextMock.Setup(mock => mock.Holdings).Returns(HoldingDbSetMock.Object);

            ModelFactoryMock = new Mock<IModelContextFactory>();
            ModelFactoryMock.Setup(mock => mock.CreateModelContext()).Returns(ModelContextMock.Object);
        }

        public void ConfigureTestHoldingData()
        {
            HoldingTestData = new[]
            {
                new Holding
                {
                    HoldingId = 1,
                    Exchange = "NASDAQ",
                    Symbol = "AAPL",
                    Quantity = 10,
                    FacebookHandle = "morgan.cabral"
                },
                new Holding
                {
                    HoldingId = 2,
                    Exchange = "NASDAQ",
                    Symbol = "TSLA",
                    Quantity = 10,
                    FacebookHandle = "morgan.cabral"
                },
                new Holding
                {
                    HoldingId = 3,
                    Exchange = "NASDAQ",
                    Symbol = "AMZN",
                    Quantity = 10,
                    FacebookHandle = "morgan.cabral"
                }
            }.AsQueryable();

            HoldingDbSetMock.Setup(mock => mock.Provider).Returns(HoldingTestData.Provider);
            HoldingDbSetMock.Setup(mock => mock.Expression).Returns(HoldingTestData.Expression);
            HoldingDbSetMock.Setup(mock => mock.ElementType).Returns(HoldingTestData.ElementType);
            HoldingDbSetMock.Setup(mock => mock.GetEnumerator()).Returns(HoldingTestData.GetEnumerator());
        }

        [Test]
        public void CreateFirstHoldingTest()
        {
            // Create some test data.
            var expected = new Holding
            {
                Exchange = "NASDAQ",
                Symbol = "GOOG",
                Quantity = 12,
                FacebookHandle = "morgan.cabral"
            };

            // Create an instance of the system to test.
            var repo = new HoldingRepository(ModelFactoryMock.Object);

            // Exercise the insert method.
            var actual = repo.Create(expected);

            // Verify behavior and state.
            HoldingDbSetMock.Verify(mock => mock.Add(expected), Times.Once);
            ModelContextMock.Verify(mock => mock.SaveChanges(), Times.Once);
        }

        [Test]
        public void CreateHoldingWithExistingTest()
        {
            // Create some test data.
            ConfigureTestHoldingData();
            var expected = new Holding
            {
                Exchange = "NASDAQ",
                Symbol = "GOOG",
                Quantity = 12,
                FacebookHandle = "morgan.cabral"
            };

            // Create an instance of the system to test.
            var repo = new HoldingRepository(ModelFactoryMock.Object);

            // Exercise the insert method.
            var actual = repo.Create(expected);

            // Verify behavior and state.
            HoldingDbSetMock.Verify(mock => mock.Add(expected), Times.Once);
            ModelContextMock.Verify(mock => mock.SaveChanges(), Times.Once);
        }

        [Test]
        public void GetHoldingsTest()
        {
            // Mocking lambda expressions doesn't really work, so we'll do this instead.
            var wasHit = false;
            Func<Holding, Boolean> filter = (holding) => { wasHit = true; return true; };

            // Create some test data.
            ConfigureTestHoldingData();
            var expected = new Holding
            {
                HoldingId = 1,
                Exchange = "NASDAQ",
                Symbol = "AAPL",
                Quantity = 10,
                FacebookHandle = "morgan.cabral"
            };

            // Create an instance of the system to test.
            var repo = new HoldingRepository(ModelFactoryMock.Object);

            // Exercise the GetHolding method.
            var actual = repo.GetHoldings("morgan.cabral", filter);

            // Verify behavior and state.
            Assert.True(wasHit);
            ModelContextMock.Verify(mock => mock.Holdings, Times.Once);
        }

        [Test]
        public void GetHoldingsAllTest()
        {
            // Create some test data.
            ConfigureTestHoldingData();
            var expected = HoldingTestData;

            // Create an instance of the system to test.
            var repo = new HoldingRepository(ModelFactoryMock.Object);

            // Exercise the GetAllHoldings method.
            var actual = repo.GetHoldings("morgan.cabral");

            // Verify behavior and state.
            ModelContextMock.Verify(mock => mock.Holdings, Times.Once);
        }

        //[Test]
        //public void UpdateHoldingTest()
        //{
        //    // Configure the Entry method so that it returns a mock an entry.
        //    ModelContextMock.Setup(mock => mock.SetModified(It.IsAny<Object>()));

        //    // Create some test data.
        //    ConfigureTestHoldingData();
        //    var expected = new Holding
        //    {
        //        HoldingId = 1,
        //        Exchange = "NASDAQ",
        //        Symbol = "AAPL",
        //        Quantity = 37, // Change 10 -> 37.
        //        FacebookHandle = "morgan.cabral"
        //    };

        //    // Create an instance of the system to test.
        //    var repo = new HoldingRepository(ModelFactoryMock.Object);

        //    // Exercise the update method.
        //    repo.Update(expected);

        //    // Verify behavior and state.
        //    HoldingDbSetMock.Verify(mock => mock.Attach(expected), Times.Once);
        //    ModelContextMock.Verify(mock => mock.SaveChanges(), Times.Once);
        //}

        [Test]
        public void DeleteHoldingTest()
        {
            // Create some test data.
            ConfigureTestHoldingData();
            var expected = HoldingTestData.Skip(1).Take(2);
            var deleted = HoldingTestData.Take(1).Single();

            // Create an instance of the system to test.
            var repo = new HoldingRepository(ModelFactoryMock.Object);

            // Exercise the delete method.
            repo.Delete(deleted);

            // Verify behavior and state.
            HoldingDbSetMock.Verify(mock => mock.Attach(deleted), Times.Once);
            HoldingDbSetMock.Verify(mock => mock.Remove(deleted), Times.Once);
            ModelContextMock.Verify(mock => mock.SaveChanges(), Times.Once);
        }
    }
}
