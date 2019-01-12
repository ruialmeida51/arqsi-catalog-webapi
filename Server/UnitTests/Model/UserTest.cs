using Server.Helper;
using Server.Model;
using Server.Service.AES;
using Server.Utils;
using Xunit;

namespace Server.UnitTests.Model
{
    public class UserTest
    {
        [Fact]
        public void ToUserToDbDtov1_Test()
        {
            var user = new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Wick",
                Email = "wicky@email.com",
                Password = SecurePasswordHasher.Hash("Moynahan"),
                Token = null,
                Role = UserRole.MANAGER
            };

            var userToDbDto = user.ToUserToDbDto();
            
            Assert.Equal(user.Id, userToDbDto.Id);
            Assert.Equal(user.FirstName, AesService.DecryptStringFromBytes_Aes(userToDbDto.FirstName));
            Assert.Equal(user.LastName, AesService.DecryptStringFromBytes_Aes(userToDbDto.LastName));
            Assert.Equal(user.Email, AesService.DecryptStringFromBytes_Aes(userToDbDto.Email));
            Assert.Equal(user.Password, AesService.DecryptStringFromBytes_Aes(userToDbDto.Password));
            if(user.Token != null)
                Assert.Equal(user.Token, AesService.DecryptStringFromBytes_Aes(userToDbDto.Token));
            Assert.Equal(user.Role, userToDbDto.Role);
        }
        
        [Fact]
        public void ToUserToDbDtov2_Test()
        {
            var user = new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Wick",
                Email = "wicky@email.com",
                Password = SecurePasswordHasher.Hash("Moynahan"),
                Token = "very strong token",
                Role = UserRole.MANAGER
            };

            var userToDbDto = user.ToUserToDbDto();
            
            Assert.Equal(user.Id, userToDbDto.Id);
            Assert.Equal(user.FirstName, AesService.DecryptStringFromBytes_Aes(userToDbDto.FirstName));
            Assert.Equal(user.LastName, AesService.DecryptStringFromBytes_Aes(userToDbDto.LastName));
            Assert.Equal(user.Email, AesService.DecryptStringFromBytes_Aes(userToDbDto.Email));
            Assert.Equal(user.Password, AesService.DecryptStringFromBytes_Aes(userToDbDto.Password));
            if(user.Token != null)
                Assert.Equal(user.Token, AesService.DecryptStringFromBytes_Aes(userToDbDto.Token));
            Assert.Equal(user.Role, userToDbDto.Role);
        }
    }
}