using System.ComponentModel.DataAnnotations;

namespace Server.Model
{
    public class Collection : DbEntity
    {
        // Collection name
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(15, ErrorMessage = "Name can't be longer than 15 characters.")]
        public string Name { get; set; }

        // Collection description
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(60, ErrorMessage = "Description can't be longer than 60 characters.")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Aesthetic line ID is required.")]
        public long AestheticLineID { get; set; }
    }
}