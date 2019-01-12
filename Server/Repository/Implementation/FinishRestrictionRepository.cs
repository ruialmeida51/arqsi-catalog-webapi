using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Model.Restriction;
using Server.Repository.Base;
using Server.Repository.Interface;

namespace Server.Repository.Implementation
{
    public class FinishRestrictionRepository : RepositoryBase<FinishRestriction>, IFinishRestrictionRepository
    {
        public FinishRestrictionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<FinishRestriction>> GetAllFinishRestrictions()
        {
            return await FindAll();
        }
        public async Task<IEnumerable<FinishRestriction>> GetFinishRestrictionById(long productID)
        {
            return await FindByCondition(finish => finish.ProductId.Equals(productID));
        }

        public async Task<bool> CreateFinishRestriction(FinishRestriction productCatalog)
        {
            return await Create(productCatalog) && await Save();
        }

        public async Task<bool> RemoveFinishRestriction(long productID)
        {
            var removedAll = true;
            var pCList = await FindByCondition(pC => pC.ProductId == productID);

            foreach (var item in pCList) removedAll = removedAll && await Remove(item);

            return removedAll;
        }
    }
}