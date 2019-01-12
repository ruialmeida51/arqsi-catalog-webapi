using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Model
{
    public class Finish : DbEntity
    {
        // Price of finish per square meter
        [Required(ErrorMessage = "Price per square meter is required.")]
        [DataType(DataType.Currency)]
        public Price PricePSM { get; set; }
        
        // Name of finish
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(15, ErrorMessage = "Name can't be longer than 15 characters.")]
        public string Name { get; set; }

        // Description of finish
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(60, ErrorMessage = "Description can't be longer than 60 characters.")]
        public string Description { get; set; }
    }
}