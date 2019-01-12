using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Model;
using Server.Repository.Base;
using Server.Repository.Interface;

namespace Server.Repository.Implementation
{
    public class CatalogRepository : RepositoryBase<Catalog>, ICatalogRepository
    {
        public CatalogRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Catalog>> GetAllCatalogs()
        {
            return (await FindAll()).OrderBy(catalog => catalog.Name);
        }

        public async Task<Catalog> GetCatalogById(long catalogID)
        {
            return (await FindByCondition(catalog => catalog.Id.Equals(catalogID))).DefaultIfEmpty(null)
                .FirstOrDefault();
        }

        public async Task<bool> CreateCatalog(Catalog catalog)
        {
            await Create(catalog);
            return await Save();
        }

        public async Task<bool> UpdateCatalog(Catalog catalog)
        {
            return await Update(catalog);
        }

        public async Task<bool> RemoveCatalog(long catalogID)
        {
            var catalog = await GetCatalogById(catalogID);
            return await Remove(catalog);
        }
    }
}