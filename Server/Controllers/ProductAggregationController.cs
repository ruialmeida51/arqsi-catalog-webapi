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
    public class ProductAggregationController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ProductAggregationController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProductAggregations()
        {
            try
            {
                var aggregations = await _repositoryWrapper.ProductAggregation.GetAllProductsAggregations();
                return aggregations != null
                    ? Ok(aggregations)
                    : StatusCode(404, new
                    {
                        message = "Internal error: Could not GET database aggregations."
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }   
        
        [AllowAnonymous]
        [HttpGet("{productID}")]
        public async Task<IActionResult> GetProductAggregationsById([FromRoute]long productID)
        {
            try
            {
                var aggregations = await _repositoryWrapper.ProductAggregation.GetProductAggregationsById(productID);
                return aggregations.Any()
                    ? Ok(aggregations)
                    : StatusCode(404, new
                    {
                        message = "Internal error: Could not GET database aggregations."
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpDelete("requester={userID}/{productID}")]
        public async Task<IActionResult> RemoveProductFromProductsAggregations([FromRoute] long productID, [FromRoute] long userID)
        {
            try
            {
                var aggregation =
                    await _repositoryWrapper.ProductAggregation.RemoveProductFromProductsAggregations(productID);

                return aggregation
                    ? Ok(new {message = "Aggregation was removed."})
                    : StatusCode(404, new {message = "Could not GET the aggregation with given ID."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpPost("requester={userID}")]
        public async Task<IActionResult> CreateProductAggregation([FromBody] ProductAggregation aggregation, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.ProductAggregation.CreateProductAggregation(aggregation)
                    ? Ok(new {message = "Created ProductAggregation.", updatedProductAggregation = aggregation})
                    : StatusCode(500, "Could not create aggregation.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
    }
}