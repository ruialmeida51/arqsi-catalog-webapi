using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Model;
using Server.Repository.Base;

namespace Server.Repository.Interface
{
    public interface IProductCollectionRepository : IRepositoryBase<ProductCollection>
    {
        Task<IEnumerable<ProductCollection>> GetAllProductCollections();
        Task<ProductCollection> GetProductCollectionById(long productID);
        Task<bool> CreateProductCollection(ProductCollection productCollection);
        Task<bool> RemoveProductCollection(long productID);
        Task<bool> RemoveOneProductFromACollection(long collectionID, long productID);
    }
}