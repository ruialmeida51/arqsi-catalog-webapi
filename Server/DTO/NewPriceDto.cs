using System;
using System.ComponentModel.DataAnnotations;

namespace Server.DTO
{
    public class NewPriceDto
    {
        [Required(ErrorMessage = "Price is required.")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        // Represents the date in which the price was created.
        [Required(ErrorMessage = "Timestamp is required.")]
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }
    }
}