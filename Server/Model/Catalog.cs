using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Model
{
    public class Catalog : DbEntity
    {
        // Catalog name
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(15, ErrorMessage = "Name can't be longer than 15 characters.")]
        public string Name { get; set; }

        // Catalog description
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(60, ErrorMessage = "Description can't be longer than 60 characters.")]
        public string Description { get; set; }

        // Catalog creation date
        [Required(ErrorMessage = "Creation Date is required.")] 
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        public Catalog()
        {
            CreationDate = DateTime.Now;
        }
    }
}