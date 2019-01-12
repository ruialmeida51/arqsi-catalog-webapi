using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.DTO;
using Server.Model;
using Server.Repository.Base;
using Server.Repository.Interface;

namespace Server.Repository.Implementation
{
    public class MaterialRepository : RepositoryBase<Material>, IMaterialRepository
    {
        public MaterialRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Material>> GetAllMaterials()
        {
            return (await FindAll()).OrderBy(material => material.Name);
        }

        public async Task<Material> GetMaterialById(long materialID)
        {
            return (await FindByCondition(material => material.Id.Equals(materialID))).DefaultIfEmpty(null).FirstOrDefault();
        }

        public async Task<bool> CreateMaterial(Material material)
        {
            await Create(material);
            return await Save();
        }

        public async Task<bool> UpdateMaterial(Material material)
        {
            return await Update(material);
        }

        public async Task<bool> RemoveMaterial(long materialID)
        {
            var material = await GetMaterialById(materialID);
            return await Remove(material);
        }

        public async Task<bool> RemoveMaterial(Material material)
        {
            return await Remove(material);
        }
        
        public async Task<bool> AddAnticipatedPrice(long materialId, NewPriceDto newPriceDto)
        {
            var material = await GetMaterialById(materialId);
            if (material == null) return false;

            var res = material.PricePSM.AddAnticipatedPrice(newPriceDto.Price, newPriceDto.Timestamp);
            if (!res) return false;

            return await UpdateMaterial(material);
        }
        
        public async Task<bool> EditPrice(long materialId, NewPriceDto newPriceDto)
        {
            var material = await GetMaterialById(materialId);
            if (material == null) return false;

            var res = material.PricePSM.EditPrice(newPriceDto.Price, newPriceDto.Timestamp);
            if (!res) return false;

            return await UpdateMaterial(material);
        }
    }
}