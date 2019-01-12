using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.DTO;
using Server.Helper;
using Server.Model;
using Server.Repository.Base;
using Server.Repository.Interface;
using Server.Service;
using Server.Service.AES;
using Server.Utils;

namespace Server.Repository.Implementation
{
    public class UserRepository : RepositoryBase<UserToDbDto>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await FindAll();
            var resUsers = users.Select(item => item.ToUser()).ToList();
            return resUsers;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return (await FindByCondition(u => AesService.DecryptStringFromBytes_Aes(u.Email).Equals(email))).DefaultIfEmpty(null).FirstOrDefault()?.ToUser();
        }

        public async Task<User> GetUserById(long userID)
        {
            return (await FindByCondition(u => u.Id.Equals(userID))).DefaultIfEmpty(null).FirstOrDefault()?.ToUser();
        }
        
        public async Task<User> GetDetachedUserById(long userID)
        {
            return (await RepositoryContext.Users.AsNoTracking().Where(u => u.Id.Equals(userID)).ToListAsync())
                .DefaultIfEmpty(null)
                .FirstOrDefault()?.ToUser();
        }

        public async Task<bool> CreateUser(User user)
        {
            var exists = await GetUserByEmail(user.Email) != null;
            if (exists) return false;

            // Hash the user password before saving into the database.
            user.Password = SecurePasswordHasher.Hash(user.Password);
            
            await Create(user.ToUserToDbDto());
            return await Save();
        }

        public async Task<bool> UpdateUser(User user)
        {
            return await Update(user.ToUserToDbDto());
        }

        public async Task<bool> RemoveUser(long userId)
        {
            var user = (await FindByCondition(u => u.Id.Equals(userId))).FirstOrDefault();
            if(user != null)
                return await Remove(user);
            return false;
        }

        public async Task<User> AuthenticateUser(string email, string password,
            IAuthenticationService authenticationService)
        {
            var user = await GetUserByEmail(email);

            // Attempt to authenticate the user. If authentication fails,
            // the method returns false. Else, returns true and adds a token
            // to the user.
            var authenticatedUser = authenticationService.Authenticate(user, password);

            //if (!await UpdateUser(authenticatedUser)) return null;
            
            return authenticatedUser;
        }
    }
}