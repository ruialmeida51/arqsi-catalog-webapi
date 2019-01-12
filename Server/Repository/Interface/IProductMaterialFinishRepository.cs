using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Model;

namespace Server.Repository.Interface
{
    public interface IProductMaterialFinishRepository
    {
        
        Task<IEnumerable<ProductMaterialFinish>> GetAllMaterialFinish();
        
        Task<IEnumerable<ProductMaterialFinish>> GetMaterialFinishOfProduct(long productId);

        Task<bool> RemoveMaterialOfProduct(long productId, long materialId);

        Task<bool> RemoveFinishOfProductsMaterial(long productId, long materialId, long finishId);

        Task<bool> CreateProductMaterialFinish(ProductMaterialFinish productMaterialFinish);

        Task<bool> RemoveByProduct(long productId);
    }
}