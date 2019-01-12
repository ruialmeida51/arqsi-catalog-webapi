using System.Collections.Generic;
using System.Threading.Tasks;
using Server.DTO;
using Server.Model;
using Server.Repository.Base;

namespace Server.Repository.Interface
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(long productID);
        Task<bool> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> RemoveProduct(long productId);
    }
}