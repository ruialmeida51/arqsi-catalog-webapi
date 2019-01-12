using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Model;
using Server.Repository.Base;

namespace Server.Repository.Interface
{
    public interface IProductCatalogRepository : IRepositoryBase<ProductCatalog>
    {
        Task<IEnumerable<ProductCatalog>> GetAllProductCatalogs();
        Task<ProductCatalog> GetProductCatalogById(long productID);
        Task<bool> CreateProductCatalog(ProductCatalog productCatalog);
        Task<bool> RemoveProductCatalog(long productID);
        Task<bool> RemoveOneProductFromACatalog(long catalogID, long productID);
    }
}