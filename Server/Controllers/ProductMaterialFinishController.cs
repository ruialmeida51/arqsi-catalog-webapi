using System;
using System.Collections.Generic;
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
    public class ProductMaterialFinishController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ProductMaterialFinishController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllMaterialFinish()
        {
            try
            {
                var materialFinishes = await _repositoryWrapper.ProductMaterialFinish.GetAllMaterialFinish();
                return materialFinishes != null
                    ? Ok(materialFinishes)
                    : StatusCode(404, new
                    {
                        message = "Internal error: Could not GET database materialFinishes."
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [AllowAnonymous]
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetAllMaterialFinishOfProduct([FromRoute] long productId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model object");

                var result = await _repositoryWrapper.ProductMaterialFinish.GetMaterialFinishOfProduct(productId);

                return result.Any()
                    ? Ok(result)
                    : StatusCode(404, new {message = "Could not GET material & finish of product."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [AllowAnonymous]
        [HttpGet("product/{productId}/material/{materialId}")]
        public async Task<IActionResult> GetFinishesOfMaterial([FromRoute] long productId, long materialId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model object");

                var materialFinishes = await _repositoryWrapper.ProductMaterialFinish.GetMaterialFinishOfProduct(productId);

                var finishList = new List<Finish>();

                var productMaterialFinishes = materialFinishes.ToList();

                for (var i = 0; i < productMaterialFinishes.Count; i++)
                {
                    var material = productMaterialFinishes.ElementAt(i);

                    if (materialId == material.MaterialId)
                    {
                        var finish = await _repositoryWrapper.Finish.GetFinishById(material.FinishId);
                        finishList.Add(finish);
                    }
                }

                return finishList.Any()
                    ? Ok(finishList)
                    : StatusCode(404, new {message = "Could not GET finishes of given material."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpPost("requester={userID}")]
        public async Task<IActionResult> CreateProductMaterialFinish(
            [FromBody] ProductMaterialFinish productMaterialFinish, [FromRoute] long userID)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model object");

                return await _repositoryWrapper.ProductMaterialFinish.CreateProductMaterialFinish(productMaterialFinish)
                    ? StatusCode(200, new {message = "Success", added = productMaterialFinish})
                    : StatusCode(500, new {message = "Error, could not create new product material finish."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpDelete("requester={userID}/product/{productId}/material/{materialId}")]
        public async Task<IActionResult> RemoveMaterialOfProduct([FromRoute] long productId,
            [FromRoute] long materialId, [FromRoute] long userID)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model object");

                var valid = await _repositoryWrapper.ProductMaterialFinish.RemoveMaterialOfProduct(productId,
                    materialId);

                return valid
                    ? StatusCode(200, new {message = "The material was successfully removed!"})
                    : StatusCode(404, "Something went wrong. Material was not removed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpDelete("requester={userID}/product/{productId}/material/{materialId}/finish/{finishId}")]
        public async Task<IActionResult> RemoveFinishOfProductsMaterial(
            [FromRoute] long productId,
            [FromRoute] long materialId,
            [FromRoute] long finishId,
            [FromRoute] long userID)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model object");

                var valid = await _repositoryWrapper.ProductMaterialFinish.RemoveFinishOfProductsMaterial(productId,
                    materialId, finishId);

                return valid
                    ? StatusCode(200, new {message = "The material & finish were successfully removed!"})
                    : StatusCode(404, "Something went wrong. Material&Finish were not removed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
    }
}