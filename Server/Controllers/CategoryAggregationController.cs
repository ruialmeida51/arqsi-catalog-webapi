

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
    public class CategoryAggregationController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CategoryAggregationController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllCategoryAggregations()
        {
            try
            {
                var aggregations = await _repositoryWrapper.CategoryAggregation.GetAllCategoriesAggregations();
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
        [HttpGet("{categoryID}")]
        public async Task<IActionResult> GetCategoryAggregationsById([FromRoute]long categoryID)
        {
            try
            {
                var aggregations = await _repositoryWrapper.CategoryAggregation.GetCategoriesAggregationsById(categoryID);
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
        [HttpDelete("requester={userID}/{categoryId}")]
        public async Task<IActionResult> RemoveCategoryFromCategoryAggregations([FromRoute] long categoryId, [FromRoute] long userID)
        {
            try
            {
                var aggregation =
                    await _repositoryWrapper.CategoryAggregation.RemoveCategoryFromCategoryAggregations(categoryId);

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
        public async Task<IActionResult> CreateCategoryAggregation([FromBody] CategoryAggregation aggregation, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.CategoryAggregation.CreateCategoryAggregation(aggregation)
                    ? Ok(new {message = "Created CategoryAggregation.", updatedProductAggregation = aggregation})
                    : StatusCode(500, "Could not create category aggregation.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
    }
}