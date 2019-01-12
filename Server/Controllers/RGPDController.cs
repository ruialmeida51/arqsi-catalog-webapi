using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Repository.Wrapper;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RGPDController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public RGPDController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet("requester={userRequester}/{userToQuery}")]
        public async Task<IActionResult> GetData([FromRoute] long userRequester, [FromRoute] long userToQuery)
        {
            try
            {
                if (userRequester != userToQuery)
                    return StatusCode(403, "You don't have permission to access this data.");

                var user = await _repositoryWrapper.User.GetUserById(userToQuery);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }


        [HttpDelete("requester={userRequester}/{userToQuery}")]
        public async Task<IActionResult> DeleteData([FromRoute] long userRequester, [FromRoute] long userToQuery)
        {
            {
                try
                {
                    if (userRequester != userToQuery)
                        return StatusCode(403, "You don't have permission to access this data.");

                    if (await _repositoryWrapper.User.RemoveUser(userToQuery))
                        return StatusCode(200, new {message = "Your data was deleted with success."});

                    return StatusCode(500, "Something went wrong, could not delete your data.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Internal server error " + ex);
                }
            }
        }
    }
}