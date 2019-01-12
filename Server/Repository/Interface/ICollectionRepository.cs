using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Model;
using Server.Repository.Base;

namespace Server.Repository.Interface
{
    public interface ICollectionRepository : IRepositoryBase<Collection>
    {
        Task<IEnumerable<Collection>> GetAllCollections();
        Task<Collection> GetCollectionById(long collectionID);
        Task<bool> CreateCollection(Collection collection);
        Task<bool> UpdateCollection(Collection collection);
        Task<bool> RemoveCollection(long collectionID); 
    }
}