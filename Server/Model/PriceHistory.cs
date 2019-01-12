using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Server.Model
{
    public class PriceHistory : DbEntity
    {
        private const double DEFAULT_CURRENT_PRICE = 0;
        
        // Price
        [DefaultValue(DEFAULT_CURRENT_PRICE)]
        [Required(ErrorMessage = "Price is required.")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        // Represents the date in which the price was created.
        [Required(ErrorMessage = "Timestamp is required.")]
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }

        public PriceHistory(double price, DateTime timestamp)
        {
            Price = price;
            Timestamp = timestamp;
        }
    }
}