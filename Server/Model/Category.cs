using System.ComponentModel.DataAnnotations;

namespace Server.Model
{
    public class Category : DbEntity
    {
        // Name of category
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(15, ErrorMessage = "Name can't be longer than 15 characters.")]
        public string Name { get; set; }
    }
}