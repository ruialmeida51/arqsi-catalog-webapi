using System.ComponentModel.DataAnnotations;

namespace Server.Model
{
    public class AestheticLine : DbEntity
    {
        // Aesthetic line name
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(15, ErrorMessage = "Name can't be longer than 15 characters.")]
        public string Name { get; set; }

        // Aesthetic line description
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(60, ErrorMessage = "Description can't be longer than 60 characters.")]
        public string Description { get; set; }
    }
}