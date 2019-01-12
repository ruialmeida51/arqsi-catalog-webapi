using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Model;
using Server.Repository.Base;
using Server.Repository.Interface;

namespace Server.Repository.Implementation
{
    public class ProductAggregationRepository : RepositoryBase<ProductAggregation>, IProductAggregationRepository
    {
        public ProductAggregationRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        
        public async Task<IEnumerable<ProductAggregation>> GetAllProductsAggregations()
        {
            return await FindAll();
        }

        public async Task<IEnumerable<ProductAggregation>> GetProductAggregationsById(long productID)
        {
            return await FindByCondition(pA => pA.RootProductId == productID);
        }
        
        public async Task<bool> CreateProductAggregation(ProductAggregation productAggregation)
        {
            return await Create(productAggregation) && await Save();
        }

        public async Task<bool> RemoveProductFromProductsAggregations(long productID)
        {
            var removedAll = true;
            var pAList = await FindByCondition(pA => pA.RootProductId == productID
                                                  || pA.SubproductId == productID);

            foreach (var item in pAList)
            {
                removedAll = removedAll && await Remove(item);
            }

            return removedAll;
        }
    }

}