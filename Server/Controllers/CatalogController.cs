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
    public class CatalogController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CatalogController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllCatalogs()
        {
            try
            {
                var catalogs = await _repositoryWrapper.Catalog.GetAllCatalogs();
                return catalogs != null
                    ? Ok(catalogs)
                    : StatusCode(404, new
                    {
                        message = "Internal error: Could not GET database catalogs."
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [AllowAnonymous]
        [HttpGet("{catalogID}")]
        public async Task<IActionResult> GetCatalogById([FromRoute] long catalogID)
        {
            try
            {
                var catalog = await _repositoryWrapper.Catalog.GetCatalogById(catalogID);
                return catalog != null
                    ? Ok(catalog)
                    : StatusCode(404, new {message = "Could not GET the catalog with given ID."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpPost("requester={userID}")]
        public async Task<IActionResult> CreateCatalog([FromBody] Catalog catalog, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.Catalog.CreateCatalog(catalog)
                    ? Ok(new {message = "Created Catalog.", createdCatalog = catalog})
                    : StatusCode(500, "Could not create catalog.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpPut("requester={userID}")]
        public async Task<IActionResult> UpdateCatalog([FromBody] Catalog catalog, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.Catalog.UpdateCatalog(catalog)
                    ? Ok(new {message = "Updated Catalog.", updatedCatalog = catalog})
                    : StatusCode(500, "Could not update catalog.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpDelete("requester={userID}/{catalogID}")]
        public async Task<IActionResult> DeleteCatalog([FromRoute] long catalogID, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.Catalog.RemoveCatalog(catalogID)
                    ? Ok(new {message = "Removed Catalog.", removedCatalog = catalogID})
                    : StatusCode(500, "Could not remove catalog.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
    }
}