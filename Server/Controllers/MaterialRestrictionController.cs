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
    public class MaterialRestrictionController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public MaterialRestrictionController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllMaterialRestrictions()
        {
            try
            {
                var materialRestrictions = await _repositoryWrapper.MaterialRestriction.GetAllMaterialRestrictions();
                return materialRestrictions != null
                    ? Ok(materialRestrictions)
                    : StatusCode(404, new
                    {
                        message = "Internal error: Could not GET database materialRestrictions."
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [AllowAnonymous]
        [HttpGet("{productID}")]
        public async Task<IActionResult> GetMaterialRestrictionById([FromRoute] long productID)
        {
            try
            {
                var materialRestriction = await _repositoryWrapper.MaterialRestriction.GetMaterialRestrictionById(productID);
                return materialRestriction.Any()
                    ? Ok(materialRestriction)
                    : StatusCode(404, new
                    {
                        message = "Internal error: Could not GET database product catalog."
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpDelete("requester={userID}/{productID}")]
        public async Task<IActionResult> RemoveMaterialRestriction([FromRoute] long productID, [FromRoute] long userID)
        {
            try
            {
                var materialRestriction =
                    await _repositoryWrapper.MaterialRestriction.RemoveMaterialRestriction(productID);

                return materialRestriction
                    ? Ok(new {message = "Product Material Restriction was removed."})
                    : StatusCode(404, new {message = "Could not GET the Product Material Restriction with given ID."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpPost("requester={userID}")]
        public async Task<IActionResult> CreateMaterialRestriction([FromBody] MaterialRestriction materialRestriction,
            [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.MaterialRestriction.CreateMaterialRestriction(materialRestriction)
                    ? Ok(new {message = "Created Product Material Restriction.", updatedMaterialRestriction = materialRestriction})
                    : StatusCode(500, "Could not create Product Material Restriction.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
    }
}
