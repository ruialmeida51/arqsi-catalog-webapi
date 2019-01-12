using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Model;
using Server.Repository.Base;
using Server.Repository.Interface;

namespace Server.Repository.Implementation
{
    public class ProductCollectionRepository : RepositoryBase<ProductCollection>, IProductCollectionRepository
    {
        public ProductCollectionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<ProductCollection>> GetAllProductCollections()
        {
            return await FindAll();
        }
        
        public async Task<ProductCollection> GetProductCollectionById(long productID)
        {
            return (await FindByCondition(productCat => productCat.CollectionId.Equals(productID))).DefaultIfEmpty(null).FirstOrDefault();
        }


        public async Task<bool> CreateProductCollection(ProductCollection productCollection)
        {
            return await Create(productCollection) && await Save();
        }

        public async Task<bool> RemoveProductCollection(long productID)
        {
            var removedAll = true;
            var pCList = await FindByCondition(pC => pC.ProductId == productID);

            foreach (var item in pCList) removedAll = removedAll && await Remove(item);

            return removedAll;
        }

        public async Task<bool> RemoveOneProductFromACollection(long collectionID, long productID)
        {
            var productCollection = (await FindByCondition(pC => pC.ProductId == productID && pC.CollectionId == collectionID)).First();
            return await Remove(productCollection);
        }
    }
}