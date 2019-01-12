using System;
using System.Linq;
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
    public class ProductCollectionController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ProductCollectionController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProductCollections()
        {
            try
            {
                var productCollections = await _repositoryWrapper.ProductCollection.GetAllProductCollections();
                return productCollections.Any()
                    ? Ok(productCollections)
                    : StatusCode(404, new
                    {
                        message = "Internal error: Could not GET database productCollections."
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [AllowAnonymous]
        [HttpGet("{productID}")]
        public async Task<IActionResult> GetProductCollectionById([FromRoute] long productID)
        {
            try
            {
                var productCatalog = await _repositoryWrapper.ProductCollection.GetProductCollectionById(productID);
                return productCatalog != null
                    ? Ok(productCatalog)
                    : StatusCode(404, new
                    {
                        message = "Internal error: Could not GET database product collection."
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] { UserRole.MANAGER })]
        [HttpDelete("requester={userID}/collection/{collectionID}/product/{productID}")]
        public async Task<IActionResult> RemoveOneProductFromACollection([FromRoute] long productID, [FromRoute] long collectionID, [FromRoute] long userID)
        {
            try
            {
                var productCollection =
                    await _repositoryWrapper.ProductCollection.RemoveOneProductFromACollection(collectionID, productID);

                return productCollection
                    ? Ok(new { message = "Product was removed from collection." })
                    : StatusCode(404, new { message = "Could not remove product from collection." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpDelete("requester={userID}/{productID}")]
        public async Task<IActionResult> RemoveProductCollection([FromRoute] long productID, [FromRoute] long userID)
        {
            try
            {
                var productCollection =
                    await _repositoryWrapper.ProductCollection.RemoveProductCollection(productID);

                return productCollection
                    ? Ok(new {message = "Product Collection was removed."})
                    : StatusCode(404, new {message = "Could not GET the Product Collection with given ID."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpPost("requester={userID}")]
        public async Task<IActionResult> CreateProductCollection([FromBody] ProductCollection productCollection,
            [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.ProductCollection.CreateProductCollection(productCollection)
                    ? Ok(new {message = "Created Product Collection.", updatedProductCollection = productCollection})
                    : StatusCode(500, "Could not create Product Collection.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
    }
}