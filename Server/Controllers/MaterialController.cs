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
    public class MaterialController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public MaterialController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllMaterials()
        {
            try
            {
                var materials = await _repositoryWrapper.Material.GetAllMaterials();
                MonitoringPricesService.VerifyPricesOfMaterials(_repositoryWrapper, materials);
                return materials != null
                    ? Ok(materials)
                    : StatusCode(404, new
                    {
                        message = "Internal error: Could not GET database materials."
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [AllowAnonymous]
        [HttpGet("{materialID}")]
        public async Task<IActionResult> GetMaterialById([FromRoute] long materialID)
        {
            try
            {
                var material = await _repositoryWrapper.Material.GetMaterialById(materialID);
                MonitoringPricesService.VerifyPricesMaterial(_repositoryWrapper, material);
                return material != null
                    ? Ok(material)
                    : StatusCode(404, new {message = "Could not GET the material with given ID."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [RequireUserRoleFilter("userID", new[] {UserRole.MANAGER})]
        [HttpPost("requester={userID}")]
        public async Task<IActionResult> CreateMaterial([FromBody] Material material, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.Material.CreateMaterial(material)
                    ? Ok(new {message = "Created Material.", createdMaterial = material})
                    : StatusCode(500, "Could not create material.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpPut("requester={userID}")]
        public async Task<IActionResult> UpdateMaterial([FromBody] Material material, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.Material.UpdateMaterial(material)
                    ? Ok(new {message = "Updated Material.", updatedMaterial = material})
                    : StatusCode(500, "Could not update material.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }

        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpDelete("requester={userID}/{materialID}")]
        public async Task<IActionResult> DeleteMaterial([FromRoute] long materialID, [FromRoute] long userID)
        {
            try
            {
                return await _repositoryWrapper.Material.RemoveMaterial(materialID)
                    ? Ok(new {message = "Removed Material.", removedMaterial = materialID})
                    : StatusCode(500, "Could not remove material.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpPut("requester={userID}/editPrice/{materialId}")]
        public async Task<IActionResult> EditPrice([FromRoute] long userID, [FromRoute] long materialId, [FromBody] NewPriceDto newPriceDto)
        {
            try
            {
                var res = await _repositoryWrapper.Material.EditPrice(materialId, newPriceDto);
                if(!res)    return StatusCode(400, "Not possible to edit price");

                return Ok(new {message = "Edited price to", newPrice = newPriceDto});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex);
            }
        }
        
        [RequireUserRoleFilter("userID", new []{UserRole.MANAGER})]
        [HttpPut("requester={userID}/addAnticipatedPrice/{materialId}")]
        public async Task<IActionResult> AddAnticipatedPrice([FromRoute] long userID, [FromRoute] long materialId, [FromBody] NewPriceDto newPriceDto)
        {
            try
            {
                var res = await _repositoryWrapper.Material.AddAnticipatedPrice(materialId, newPriceDto);
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