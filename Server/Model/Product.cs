using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Server.Model
{
    public class Product : DbEntity
    {
        private const double DEFAULT_MIN_OCCUPATION = 0;
        private const double DEFAULT_MAX_OCCUPATION = 100;

        // Product name
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(15, ErrorMessage = "Name can't be longer than 15 characters.")]
        public string Name { get; set; }

        // Product description
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(60, ErrorMessage = "Description can't be longer than 60 characters.")]
        public string Description { get; set; }

        // Min occupation
        [DefaultValue(DEFAULT_MIN_OCCUPATION)]
        [Required(ErrorMessage = "Minimum occupation is required.")]
        [Range(0,100, ErrorMessage = "Minimum occupation must be between {1} and {2}.")]
        public double MinOccupation { get; set; }
        
        // Max occupation
        [DefaultValue(DEFAULT_MAX_OCCUPATION)]
        [Required(ErrorMessage = "Maximum occupation is required.")]
        [Range(0,100, ErrorMessage = "Maximum occupation must be between {1} and {2}.")]
        public double MaxOccupation { get; set; }
        
        [Required(ErrorMessage = "Category is required.")]
        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        
        // Dimension of product
        public Dimension Dimension{ get; set; }

    }
}