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
    public class ProductCatalogController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ProductCatalogController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProductCatalogs()
        {
            try
            {
                var productCatalogs = await _repositoryWrapper.ProductCatalog.GetAllProductCatalogs();
                return productCatalogs != null
                    ? Ok(productCatalogs)
                    : StatusCode(404, new
                    {
                        message = "Internal error: Could not GET database productCatalogs."
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }        
        [AllowAnonymous]
        [HttpGet("{productID}")]
        public async Task<IActionResult> GetProductCatalogById([FromRoute]long productID)
        {
            try
            {
                var productCatalog = await _repositoryWrapper.ProductCatalog.GetProductCatalogById(productID);
                return productCatalog != null
                    ? Ok(productCatalog)
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

        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpDelete("requester={userID}/catalog/{catalogID}/product/{productID}")]
        public async Task<IActionResult> RemoveOneProductFromACatalog([FromRoute] long productID, [FromRoute] long catalogID, [FromRoute] long userID)
        {
            try
            {
                var productCatalog =
                    await _repositoryWrapper.ProductCatalog.RemoveOneProductFromACatalog(catalogID, productID);

                return productCatalog
                    ? Ok(new {message = "Product was removed from catalog."})
                    : StatusCode(404, new {message = "Could not remove product from catalog."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpDelete("requester={userID}/{productID}")]
        public async Task<IActionResult> RemoveProductCatalog([FromRoute] long productID, [FromRoute] long userID)
        {
            try
            {
                var productCatalog =
                    await _repositoryWrapper.ProductCatalog.RemoveProductCatalog(productID);

                return productCatalog
                    ? Ok(new {message = "Product Catalog was removed."})
                    : StatusCode(404, new {message = "Could not GET the Product Catalog with given ID."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpPost("requester={userID}")]
        public async Task<IActionResult> CreateProductCatalog([FromBody] ProductCatalog productCatalog, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.ProductCatalog.CreateProductCatalog(productCatalog)
                    ? Ok(new {message = "Created Product Catalog.", updatedProductCatalog = productCatalog})
                    : StatusCode(500, "Could not create Product Catalog.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
    }
}