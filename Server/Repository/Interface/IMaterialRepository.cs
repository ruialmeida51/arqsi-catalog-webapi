using System.Collections.Generic;
using System.Threading.Tasks;
using Server.DTO;
using Server.Model;
using Server.Repository.Base;

namespace Server.Repository.Interface
{
    public interface IMaterialRepository : IRepositoryBase<Material>
    {
        Task<IEnumerable<Material>> GetAllMaterials();
        Task<Material> GetMaterialById(long materialID);
        Task<bool> CreateMaterial(Material material);
        Task<bool> UpdateMaterial(Material material);
        Task<bool> RemoveMaterial(long materialID);
        Task<bool> AddAnticipatedPrice(long materialId, NewPriceDto newPriceDto);
        Task<bool> EditPrice(long materialId, NewPriceDto newPriceDto);
    }
}