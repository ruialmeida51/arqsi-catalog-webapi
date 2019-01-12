using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Model.Restriction;
using Server.Repository.Base;

namespace Server.Repository.Interface
{
    public interface IFinishRestrictionRepository : IRepositoryBase<FinishRestriction>
    {
        Task<IEnumerable<FinishRestriction>> GetAllFinishRestrictions();
        Task<IEnumerable<FinishRestriction>> GetFinishRestrictionById(long productID);
        Task<bool> CreateFinishRestriction(FinishRestriction finishRestriction);
        Task<bool> RemoveFinishRestriction(long productID);
    }
}