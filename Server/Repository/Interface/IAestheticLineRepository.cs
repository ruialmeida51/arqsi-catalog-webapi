using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Model;
using Server.Repository.Base;

namespace Server.Repository.Interface
{
    public interface IAestheticLineRepository : IRepositoryBase<AestheticLine>
    {
        Task<IEnumerable<AestheticLine>> GetAllAestheticLines();
        Task<AestheticLine> GetAestheticLineById(long aestheticLineID);
        Task<bool> CreateAestheticLine(AestheticLine aestheticLine);
        Task<bool> UpdateAestheticLine(AestheticLine aestheticLine);
        Task<bool> RemoveAestheticLine(long aestheticLineID); 
    }
}