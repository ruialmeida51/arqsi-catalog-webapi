using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Model;
using Server.Repository.Base;

namespace Server.Repository.Interface
{
    public interface IProductAggregationRepository : IRepositoryBase<ProductAggregation>
    {
        Task<IEnumerable<ProductAggregation>> GetAllProductsAggregations();
        Task<IEnumerable<ProductAggregation>> GetProductAggregationsById(long productID);
        Task<bool> CreateProductAggregation(ProductAggregation productAggregation);
        Task<bool> RemoveProductFromProductsAggregations(long productID);
    }
}