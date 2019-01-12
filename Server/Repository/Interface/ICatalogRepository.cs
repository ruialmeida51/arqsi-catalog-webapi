using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Model;
using Server.Repository.Base;

namespace Server.Repository.Interface
{
    public interface ICatalogRepository : IRepositoryBase<Catalog>
    {
        Task<IEnumerable<Catalog>> GetAllCatalogs();
        Task<Catalog> GetCatalogById(long catalogID);
        Task<bool> CreateCatalog(Catalog catalog);
        Task<bool> UpdateCatalog(Catalog catalog);
        Task<bool> RemoveCatalog(long catalogID);    
    }
}