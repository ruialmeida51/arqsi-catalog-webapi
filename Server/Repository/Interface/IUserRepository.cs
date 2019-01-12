using System.Collections.Generic;
using System.Threading.Tasks;
using Server.DTO;
using Server.Model;
using Server.Repository.Base;
using Server.Service;

namespace Server.Repository.Interface
{
    public interface IUserRepository : IRepositoryBase<UserToDbDto>
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(long userID);
        Task<bool> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> RemoveUser(long userId);
        Task<User> AuthenticateUser(string email, string password, IAuthenticationService authenticationService);
        Task<User> GetDetachedUserById(long userID);
    }
}