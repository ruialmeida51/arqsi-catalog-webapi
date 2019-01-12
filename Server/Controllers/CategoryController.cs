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
    public class CategoryController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CategoryController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _repositoryWrapper.Category.GetAllCategories();
                return categories != null
                    ? Ok(categories)
                    : StatusCode(404, new
                    {
                        message = "Internal error: Could not GET database categories."
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [AllowAnonymous]
        [HttpGet("{categoryID}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] long categoryID)
        {
            try
            {
                var category = await _repositoryWrapper.Category.GetCategoryById(categoryID);
                return category != null
                    ? Ok(category)
                    : StatusCode(404, new {message = "Could not GET the category with given ID."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpPost("requester={userID}")]
        public async Task<IActionResult> CreateCategory([FromBody] Category category, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.Category.CreateCategory(category)
                    ? Ok(new {message = "Created Category.", createdCategory = category})
                    : StatusCode(500, "Could not create category.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpPut("requester={userID}")]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.Category.UpdateCategory(category)
                    ? Ok(new {message = "Updated Category.", updatedCategory = category})
                    : StatusCode(500, "Could not update category.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpDelete("requester={userID}/{categoryID}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] long categoryID, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.Category.RemoveCategory(categoryID)
                    ? Ok(new {message = "Removed Category.", removedCategory = categoryID})
                    : StatusCode(500, "Could not remove category.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
    }
}