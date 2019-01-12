using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Server.DTO;
using Server.Service.AES;
using Server.Utils;

namespace Server.Model
{
    public class User : DbEntity
    {
        // User's first name.
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(15, ErrorMessage = "First Name can't be longer than 15 characters.")]
        public string FirstName { get; set; }        
        
        // User's last name.
        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(15, ErrorMessage = "Last Name can't be longer than 15 characters.")]
        public string LastName { get; set; }
        
        // User's email.
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        
        // User's password.
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        // User's authentication token.
        public string Token { get; set; }
        
        // User's role.
        [Required(ErrorMessage = "User role is required")]
        [EnumDataType(typeof(UserRole))]
        public UserRole Role { get; set; }

        public User() { Role = UserRole.CLIENT; }

        public UserToDbDto ToUserToDbDto()
        {
            var userToDb = new UserToDbDto
            {
                FirstName = AesService.EncryptStringToBytes_Aes(FirstName),
                LastName = AesService.EncryptStringToBytes_Aes(LastName),
                Email = AesService.EncryptStringToBytes_Aes(Email),
                Password = AesService.EncryptStringToBytes_Aes(Password)
            };

            if(Token != null)
                    userToDb.Token = AesService.EncryptStringToBytes_Aes(Token);

            userToDb.Id = Id;
            userToDb.Role = Role;
            return userToDb;
        }
    }
}