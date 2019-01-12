using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Model.Restriction;
using Server.Repository.Base;
using Server.Repository.Interface;

namespace Server.Repository.Implementation
{
    public class MaterialRestrictionRepository : RepositoryBase<MaterialRestriction>, IMaterialRestrictionRepository
    {
        public MaterialRestrictionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<MaterialRestriction>> GetAllMaterialRestrictions()
        {
            return await FindAll();
        }

        public async Task<IEnumerable<MaterialRestriction>> GetMaterialRestrictionById(long productID)
        {
            return await FindByCondition(material => material.ProductId.Equals(productID));
        }

        public async Task<bool> CreateMaterialRestriction(MaterialRestriction productCatalog)
        {
            return await Create(productCatalog) && await Save();
        }

        public async Task<bool> RemoveMaterialRestriction(long productID)
        {
            var removedAll = true;
            var pCList = await FindByCondition(pC => pC.ProductId == productID);

            foreach (var item in pCList) removedAll = removedAll && await Remove(item);

            return removedAll;
        }
    }
}