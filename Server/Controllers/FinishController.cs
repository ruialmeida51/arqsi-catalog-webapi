using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DataAnnotation;
using Server.DTO;
using Server.Model;
using Server.Repository.Wrapper;
using Server.Service.MonitoringPricesService;
using Server.Utils;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FinishController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public FinishController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllFinishes()
        {
            try
            {
                var finishes = await _repositoryWrapper.Finish.GetAllFinishes();
                MonitoringPricesService.VerifyPricesOfFinishes(_repositoryWrapper, finishes);
                
                return finishes != null
                    ? Ok(finishes)
                    : StatusCode(404, new
                    {
                        message = "Internal error: Could not GET database finishes."
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [AllowAnonymous]
        [HttpGet("{finishID}")]
        public async Task<IActionResult> GetFinishById([FromRoute] long finishID)
        {
            try
            {
                var finish = await _repositoryWrapper.Finish.GetFinishById(finishID);
                MonitoringPricesService.VerifyPricesFinish(_repositoryWrapper, finish);
                
                return finish != null
                    ? Ok(finish)
                    : StatusCode(404, new {message = "Could not GET the finish with given ID."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpPost("requester={userID}")]
        public async Task<IActionResult> CreateFinish([FromBody] Finish finish, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.Finish.CreateFinish(finish)
                    ? Ok(new {message = "Created Finish.", createdFinish = finish})
                    : StatusCode(500, "Could not create finish.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpPut("requester={userID}")]
        public async Task<IActionResult> UpdateFinish([FromBody] Finish finish, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.Finish.UpdateFinish(finish)
                    ? Ok(new {message = "Updated Finish.", updatedFinish = finish})
                    : StatusCode(500, "Could not update finish.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpDelete("requester={userID}/{finishID}")]
        public async Task<IActionResult> DeleteFinish([FromRoute] long finishID, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.Finish.RemoveFinish(finishID)
                    ? Ok(new {message = "Removed Finish.", removedFinish = finishID})
                    : StatusCode(500, "Could not remove finish.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpPut("requester={userID}/editPrice/{finishId}")]
        public async Task<IActionResult> EditPrice([FromRoute] long userID, [FromRoute] long finishId, [FromBody] NewPriceDto newPriceDto)
        {
            try
            {
                var res = await _repositoryWrapper.Finish.EditPrice(finishId, newPriceDto);
                if(!res)    return StatusCode(400, "Not possible to edit price");

                return Ok(new {message = "Edited price to", newPrice = newPriceDto});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpPut("requester={userID}/addAnticipatedPrice/{finishId}")]
        public async Task<IActionResult> AddAnticipatedPrice([FromRoute] long userID, [FromRoute] long finishId, [FromBody] NewPriceDto newPriceDto)
        {
            try
            {
                var res = await _repositoryWrapper.Finish.AddAnticipatedPrice(finishId, newPriceDto);
                if(!res)    return StatusCode(400, "Not possible to edit price");

                return Ok(new {message = "Edited price to", newPrice = newPriceDto});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
    }
}