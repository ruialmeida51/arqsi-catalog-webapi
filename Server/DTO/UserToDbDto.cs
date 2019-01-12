using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using Server.Model;
using Server.Service.AES;
using Server.Utils;

namespace Server.DTO
{
    public class UserToDbDto : DbEntity
    {
        public byte[] FirstName { get; set; }
        
        public byte[] LastName { get; set; }
        
        public byte[] Email { get; set; }
        
        public byte[] Password { get; set; }
        
        public byte[] Token { get; set; }
        
        public UserRole Role { get; set; }

        public User ToUser()
        {
            var user = new User
            {
                FirstName = AesService.DecryptStringFromBytes_Aes(FirstName),
                LastName = AesService.DecryptStringFromBytes_Aes(LastName),
                Email = AesService.DecryptStringFromBytes_Aes(Email),
                Password = AesService.DecryptStringFromBytes_Aes(Password)
            };

            if(Token != null)
                user.Token = AesService.DecryptStringFromBytes_Aes(Token);

            user.Id = Id;
            user.Role = Role;
            return user;
        }
    }
}