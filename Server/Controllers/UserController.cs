using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DataAnnotation;
using Server.DTO;
using Server.Model;
using Server.Repository.Wrapper;
using Server.Service;
using Server.Utils;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IAuthenticationService _authenticationService;

        public UserController(IRepositoryWrapper repositoryWrapper, IAuthenticationService authenticationService)
        {
            _repositoryWrapper = repositoryWrapper;
            _authenticationService = authenticationService;
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpGet("requester={userID}")]
        public async Task<IActionResult> GetAllUsers([FromRoute] long userID)
        {
            try
            {
                var users = await _repositoryWrapper.User.GetAllUsers();
                return users != null
                    ? Ok(users)
                    : StatusCode(404, new {message = "Internal error: Could not GET database users."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpGet("requester={userID}/id={userToFind}")]
        public async Task<IActionResult> GetUserById([FromRoute] long userToFind, [FromRoute] long userID)
        {
            try
            {
                var user = await _repositoryWrapper.User.GetUserById(userToFind);
                return user != null
                    ? Ok(user)
                    : StatusCode(404, new {message = "Could not GET the user with given ID."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpGet("requester={userID}/email={email}")]
        public async Task<IActionResult> GetUserByEmail([FromRoute] string email, [FromRoute]long userID)
        {
            try
            {
                var user = await _repositoryWrapper.User.GetUserByEmail(email);
                return user != null
                    ? Ok(user)
                    : StatusCode(404, new {message = "Could not GET the user with given email."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpPut("requester={userID}")]
        public async Task<IActionResult> UpdateUser([FromBody] User user, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.User.UpdateUser(user)
                    ? Ok(new {message = "Updated User.", updatedUser = user.Email})
                    : StatusCode(500, "Could not update user.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("requester", new[] {UserRole.MANAGER})]
        [HttpDelete("requester={requester}/{userToDelete}")]
        public async Task<IActionResult> DeleteUser([FromRoute] long requester, [FromRoute] long userToDelete)
        {
            try
            {
                return await _repositoryWrapper.User.RemoveUser(userToDelete)
                    ? Ok(new {message = "Removed User.", removedUser = userToDelete})
                    : StatusCode(500, "Could not remove user.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid user model object.");

                var result = await _repositoryWrapper.User.CreateUser(user);

                return result
                    ? Ok(new 
                    {
                        message = user.FirstName + " " + user.LastName + ", your account was registered successfully.", 
                        email = user.Email
                        
                    })
                    : StatusCode(500, new {message = "Could not register new user. This probably means that an user is already registered with the email provided."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserDTO user)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid user model object.");

                var userWithToken =
                    await _repositoryWrapper.User.AuthenticateUser(user.Email, user.Password, _authenticationService);
                return userWithToken == null
                    ? StatusCode(401, new {message = "Not Authenticated. Either your email isn't registered or the password is invalid." })
                    : Ok(new {message = "Authenticated", token = userWithToken.Token, userID = userWithToken.Id, role = userWithToken.Role});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
    }
}