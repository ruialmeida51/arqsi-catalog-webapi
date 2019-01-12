using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Model;
using Server.Repository.Base;
using Server.Repository.Interface;

namespace Server.Repository.Implementation
{
    public class ProductCatalogRepository : RepositoryBase<ProductCatalog>, IProductCatalogRepository
    {
        public ProductCatalogRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<ProductCatalog>> GetAllProductCatalogs()
        {
            return await FindAll();
        }

        public async Task<ProductCatalog> GetProductCatalogById(long productID)
        {
            return (await FindByCondition(productCat => productCat.CatalogId.Equals(productID))).DefaultIfEmpty(null)
                .FirstOrDefault();
        }

        public async Task<bool> CreateProductCatalog(ProductCatalog productCatalog)
        {
            return await Create(productCatalog) && await Save();
        }

        public async Task<bool> RemoveProductCatalog(long productID)
        {
            var removedAll = true;
            var pCList = await FindByCondition(pC => pC.ProductId == productID);

            foreach (var item in pCList) removedAll = removedAll && await Remove(item);

            return removedAll;
        }

        public async Task<bool> RemoveOneProductFromACatalog(long catalogID, long productID)
        {
            var productCatalog = (await FindByCondition(pC => pC.ProductId == productID && pC.CatalogId == catalogID)).First();
            return await Remove(productCatalog);
        }
    }
}