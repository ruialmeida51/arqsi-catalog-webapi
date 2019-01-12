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
    public class CollectionController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CollectionController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllCollections()
        {
            try
            {
                var collections = await _repositoryWrapper.Collection.GetAllCollections();
                return collections != null
                    ? Ok(collections)
                    : StatusCode(404, new
                    {
                        message = "Internal error: Could not GET database collections."
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [AllowAnonymous]
        [HttpGet("{collectionID}")]
        public async Task<IActionResult> GetCollectionById([FromRoute] long collectionID)
        {
            try
            {
                var collection = await _repositoryWrapper.Collection.GetCollectionById(collectionID);
                return collection != null
                    ? Ok(collection)
                    : StatusCode(404, new {message = "Could not GET the collection with given ID."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpPost("requester={userID}")]
        public async Task<IActionResult> CreateCollection([FromBody] Collection collection, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.Collection.CreateCollection(collection)
                    ? Ok(new {message = "Created Collection.", createdCollection = collection})
                    : StatusCode(500, "Could not create collection.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpPut("requester={userID}")]
        public async Task<IActionResult> UpdateCollection([FromBody] Collection collection, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.Collection.UpdateCollection(collection)
                    ? Ok(new {message = "Updated Collection.", updatedCollection = collection})
                    : StatusCode(500, "Could not update collection.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpDelete("requester={userID}/{collectionID}")]
        public async Task<IActionResult> DeleteCollection([FromRoute] long collectionID, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.Collection.RemoveCollection(collectionID)
                    ? Ok(new {message = "Removed Collection.", removedCollection = collectionID})
                    : StatusCode(500, "Could not remove collection.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
    }
}