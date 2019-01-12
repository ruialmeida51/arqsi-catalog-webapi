using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Model;
using Server.Repository.Base;
using Server.Repository.Interface;

namespace Server.Repository.Implementation
{
    public class ProductMaterialFinishRepository : RepositoryBase<ProductMaterialFinish>, IProductMaterialFinishRepository
    {
        public ProductMaterialFinishRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<ProductMaterialFinish>> GetAllMaterialFinish()
        {
            return await FindAll();
        }

        public async Task<IEnumerable<ProductMaterialFinish>> GetMaterialFinishOfProduct(long productId)
        {
            return await FindByCondition(pMF => pMF.ProductId.Equals(productId));
        }

        public async Task<bool> RemoveMaterialOfProduct(long productId, long materialId)
        {
            var pmf = 
                (await FindByCondition(pMF => pMF.ProductId == productId && pMF.MaterialId == materialId)).First();

            return await Remove(pmf);
        }
        
        public async Task<bool> RemoveFinishOfProductsMaterial(long productId, long materialId, long finishId)
        {
            var pmf = 
                (await FindByCondition(pMF => pMF.ProductId == productId 
                                              && pMF.MaterialId == materialId
                                              && pMF.FinishId == finishId)).First();

            return await Remove(pmf);
        }
        
        public async Task<bool> RemoveByProduct(long productId)
        {
            var pmfs = (await FindByCondition(pMF => pMF.ProductId == productId)).ToList();
            var isDeletedSuccessfully = true;
            foreach (var item in pmfs)
            {
                isDeletedSuccessfully = isDeletedSuccessfully && await Remove(item);
            }

            return isDeletedSuccessfully;
        }
        
        public async Task<bool> CreateProductMaterialFinish(ProductMaterialFinish productMaterialFinish)
        {
            return await Create(productMaterialFinish) && await Save();
        }
    }
}