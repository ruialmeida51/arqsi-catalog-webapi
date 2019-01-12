using System.ComponentModel.DataAnnotations;

namespace Server.DTO
{
    public class UserDTO
    {        
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }        
        
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}