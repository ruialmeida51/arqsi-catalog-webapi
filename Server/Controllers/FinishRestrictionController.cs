using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DataAnnotation;
using Server.Model.Restriction;
using Server.Repository.Wrapper;
using Server.Utils;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FinishRestrictionController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public FinishRestrictionController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllFinishRestrictions()
        {
            try
            {
                var finishRestrictions = await _repositoryWrapper.FinishRestriction.GetAllFinishRestrictions();
                return finishRestrictions != null
                    ? Ok(finishRestrictions)
                    : StatusCode(404, new
                    {
                        message = "Internal error: Could not GET database finishRestrictions."
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [AllowAnonymous]
        [HttpGet("{productID}")]
        public async Task<IActionResult> GetFinishRestrictionById([FromRoute] long productID)
        {
            try
            {
                var finishRestriction = await _repositoryWrapper.FinishRestriction.GetFinishRestrictionById(productID);
                
                return finishRestriction.Any()
                    ? Ok(finishRestriction)
                    : StatusCode(404, new { message = "Could not find database product catalog." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpDelete("requester={userID}/{productID}")]
        public async Task<IActionResult> RemoveFinishRestriction([FromRoute] long productID, [FromRoute] long userID)
        {
            try
            {
                var finishRestriction =
                    await _repositoryWrapper.FinishRestriction.RemoveFinishRestriction(productID);

                return finishRestriction
                    ? Ok(new {message = "Product Finish Restriction was removed."})
                    : StatusCode(404, new {message = "Could not GET the Product Finish Restriction with given ID."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpPost("requester={userID}")]
        public async Task<IActionResult> CreateFinishRestriction([FromBody] FinishRestriction finishRestriction,
            [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.FinishRestriction.CreateFinishRestriction(finishRestriction)
                    ? Ok(new {message = "Created Product Finish Restriction.", updatedFinishRestriction = finishRestriction})
                    : StatusCode(500, "Could not create Product Finish Restriction.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
    }
}