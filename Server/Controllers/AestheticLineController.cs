using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DataAnnotation;
using Server.Model;
using Server.Repository.Wrapper;
using Server.Utils;

namespace Server.Controllers
{   
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AestheticLineController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public AestheticLineController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAestheticLines()
        {
            try
            {
                var aestheticLines = await _repositoryWrapper.AestheticLine.GetAllAestheticLines();
                return aestheticLines != null
                    ? Ok(aestheticLines)
                    : StatusCode(404, new
                    {
                        message = "Internal error: Could not GET database aestheticLines."
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [AllowAnonymous]
        [HttpGet("{aestheticLineID}")]
        public async Task<IActionResult> GetAestheticLineById([FromRoute] long aestheticLineID)
        {
            try
            {
                var aestheticLine = await _repositoryWrapper.AestheticLine.GetAestheticLineById(aestheticLineID);
                return aestheticLine != null
                    ? Ok(aestheticLine)
                    : StatusCode(404, new {message = "Could not GET the aestheticLine with given ID."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpPost("requester={userID}")]
        public async Task<IActionResult> CreateAestheticLine([FromBody] AestheticLine aestheticLine, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.AestheticLine.CreateAestheticLine(aestheticLine)
                    ? Ok(new {message = "Created AestheticLine.", createdAestheticLine = aestheticLine})
                    : StatusCode(500, "Could not create aestheticLine.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpPut("requester={userID}")]
        public async Task<IActionResult> UpdateAestheticLine([FromBody] AestheticLine aestheticLine, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.AestheticLine.UpdateAestheticLine(aestheticLine)
                    ? Ok(new {message = "Updated AestheticLine.", updatedAestheticLine = aestheticLine})
                    : StatusCode(500, "Could not update aestheticLine.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpDelete("requester={userID}/{aestheticLineID}")]
        public async Task<IActionResult> DeleteAestheticLine([FromRoute] long aestheticLineID, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.AestheticLine.RemoveAestheticLine(aestheticLineID)
                    ? Ok(new {message = "Removed AestheticLine.", removedAestheticLine = aestheticLineID})
                    : StatusCode(500, "Could not remove aestheticLine.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }  
    }
}