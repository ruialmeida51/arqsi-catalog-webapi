using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Model
{
    public class Price : DbEntity
    {
        // Current price
        [Required(ErrorMessage = "Current value is required.")]
        [DataType(DataType.Currency)]
        public double CurrentValue { get; set; }
        
        // Date time where price was set
        // Represents the date in which the price was created.
        [Required(ErrorMessage = "Timestamp is required.")]
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }

        // Old prices
        public ICollection<PriceHistory> PriceHistoryPast { get; set; }

        // Upcoming prices
        public ICollection<PriceHistory> PriceHistoryFuture { get; set; }

        // Edit current price to the newest price
        public bool EditPrice(double newPrice, DateTime timestamp)
        {
            if (Timestamp > timestamp) return false;
            
            PriceHistoryPast.Add(new PriceHistory(CurrentValue, Timestamp));

            CurrentValue = newPrice;
            Timestamp = timestamp;
            
            return true;
        }
        
        // Edit anticipated price
        public bool AddAnticipatedPrice(double newPrice, DateTime timestamp)
        {
            if (DateTime.Now > timestamp) return false;
            
            PriceHistoryFuture.Add(new PriceHistory(newPrice, timestamp));
            
            return true;
        }
    }
}