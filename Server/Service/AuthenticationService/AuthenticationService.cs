using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Helper;
using Server.Model;
using Server.Repository.Implementation;

namespace Server.Service.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings _appSettings;

        public AuthenticationService(IOptions<AppSettings> appSettings) 
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(User user, string password)
        {
            // If user is not found, return null.
            if (user == null) return null;
            
            // If the passwords don't match cancel operation.
            if (!SecurePasswordHasher.Verify(password, user.Password)) return null;
            
            // At this point the credentials are valid, so we can begin the creation of a JWT token.
            var tokenHandler = new JwtSecurityTokenHandler();
            
            // Encode it in ASCII using the key stored in the app settings.
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            
            // Create the new Token and assign in to an user.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Assign it to the user ID.
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, user.Id.ToString()) }),
                
                // Set to expire after 2 hours.
                Expires = DateTime.UtcNow.AddHours(2),
                
                // Hash it in 256 BIT SHA
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            // Get token object
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            // Assign token to user.
            user.Token = tokenHandler.WriteToken(token);
            
            return user;
        }
    }
}