using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Internal;
using Server.DataAnnotation;
using Server.DTO;
using Server.Model;
using Server.Repository.Wrapper;
using Server.Utils;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ProductController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model object");
                var result = await _repositoryWrapper.Product.GetAllProducts();

                return result.Any()
                    ? Ok(result)
                    : StatusCode(404, new {message = "Could not find products."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model object");

                var product = await _repositoryWrapper.Product.GetProductById(id);

                if (product == null) return StatusCode(404, "Product not found");
                product.Category = await _repositoryWrapper.Category.GetCategoryById(product.CategoryId);

                if (product.Category != null)
                    return Ok(new {message = "Success.", Product = product});
                return StatusCode(404, "Product category not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpPost("requester={userID}")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product, [FromRoute] long userID)
        {
            try
            {
                if (product == null) return BadRequest("Product object is null.");

                if (!ModelState.IsValid) return BadRequest("Invalid model object.");

                await _repositoryWrapper.Product.CreateProduct(product);

                return CreatedAtAction("CreateProduct", new {id = product.Id}, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpPut("requester={userID}")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product, [FromRoute] long userID)
        {
            try
            {
                if (product == null) return BadRequest("Product object is null.");

                if (!ModelState.IsValid) return BadRequest("Invalid model object.");

                return await _repositoryWrapper.Product.UpdateProduct(product)
                    ? StatusCode(200, new {id = product.Id, updated = product})
                    : StatusCode(500, "Not possible to update product");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpDelete("requester={userID}/{productID}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] long productID, [FromRoute] long userID)
        {
            try
            {
                var product = await _repositoryWrapper.Product.RemoveProduct(productID) &&
                               await _repositoryWrapper.ProductCatalog.RemoveProductCatalog(productID) &&
                               await _repositoryWrapper.ProductCollection.RemoveProductCollection(productID) &&
                               await _repositoryWrapper.ProductAggregation.RemoveProductFromProductsAggregations(productID) &&
                               await _repositoryWrapper.FinishRestriction.RemoveFinishRestriction(productID) &&
                               await _repositoryWrapper.MaterialRestriction.RemoveMaterialRestriction(productID) && 
                               await _repositoryWrapper.ProductMaterialFinish.RemoveByProduct(productID);

                return product
                    ? Ok(new {message = "Product and its related tables were removed."})
                    : StatusCode(500, new {message = "Something went wrong while deleting the product and its related tables."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
    }
}