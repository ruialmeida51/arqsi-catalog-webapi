using System;
using System.Collections.Generic;
using Server.Model;
using Xunit;

namespace Server.UnitTests.Model
{
    public class PriceTest
    {
        [Fact]
        public void Edit_NonValid_Price_Test()
        {
            var price = new Price
            {
                CurrentValue = 20, 
                Timestamp = new DateTime(2019, 1, 1, 20, 0, 0),
                PriceHistoryFuture = new List<PriceHistory>(),
                PriceHistoryPast = new List<PriceHistory>()
            };

            var newPrice = 21;
            var timestamp = new DateTime(2019, 1, 1, 19, 59, 59);
            
            Assert.False(price.EditPrice(newPrice, timestamp));
        }
        
        [Fact]
        public void Edit_Valid_Price_Test()
        {
            var price = new Price
            {
                CurrentValue = 20, 
                Timestamp = new DateTime(2019, 1, 1, 20, 0, 0),
                PriceHistoryFuture = new List<PriceHistory>(),
                PriceHistoryPast = new List<PriceHistory>()
            };

            var newPrice = 21;
            var timestamp = new DateTime(2019, 1, 1, 20, 1, 1);
            
            Assert.True(price.EditPrice(newPrice, timestamp));
        }
        
        [Fact]
        public void Edit_NonValid_AnticipatedPrice_Test()
        {
            var price = new Price
            {
                CurrentValue = 20, 
                Timestamp = new DateTime(2019, 1, 1, 20, 0, 0),
                PriceHistoryFuture = new List<PriceHistory>(),
                PriceHistoryPast = new List<PriceHistory>()
            };

            var newPrice = 21;
            var timestamp = DateTime.Now.Subtract(TimeSpan.FromSeconds(100));
            
            Assert.False(price.AddAnticipatedPrice(newPrice, timestamp));
        }
        
        [Fact]
        public void Edit_Valid_AnticipatedPrice_Test()
        {
            var price = new Price
            {
                CurrentValue = 20, 
                Timestamp = new DateTime(2019, 1, 1, 20, 0, 0),
                PriceHistoryFuture = new List<PriceHistory>(),
                PriceHistoryPast = new List<PriceHistory>()
            };

            var newPrice = 21;
            var timestamp = DateTime.Now.Add(TimeSpan.FromSeconds(1));
            
            Assert.True(price.AddAnticipatedPrice(newPrice, timestamp));
        }
    }
}