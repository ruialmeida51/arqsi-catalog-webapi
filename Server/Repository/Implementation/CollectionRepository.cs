using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Model;
using Server.Repository.Base;
using Server.Repository.Interface;

namespace Server.Repository.Implementation
{
    public class CollectionRepository : RepositoryBase<Collection>, ICollectionRepository
    {
        public CollectionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Collection>> GetAllCollections()
        {
            return (await FindAll()).OrderBy(collection => collection.Name);
        }

        public async Task<Collection> GetCollectionById(long collectionID)
        {
            return (await FindByCondition(collection => collection.Id.Equals(collectionID))).DefaultIfEmpty(null)
                .FirstOrDefault();
        }

        public async Task<bool> CreateCollection(Collection collection)
        {
            await Create(collection);
            return await Save();
        }

        public async Task<bool> UpdateCollection(Collection collection)
        {
            return await Update(collection);
        }

        public async Task<bool> RemoveCollection(long collectionID)
        {
            var collection = await GetCollectionById(collectionID);
            return await Remove(collection);
        }
    }
}