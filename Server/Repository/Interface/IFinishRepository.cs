using System.Collections.Generic;
using System.Threading.Tasks;
using Server.DTO;
using Server.Model;
using Server.Repository.Base;

namespace Server.Repository.Interface
{
    public interface IFinishRepository : IRepositoryBase<Finish>
    {
        Task<IEnumerable<Finish>> GetAllFinishes();
        Task<Finish> GetFinishById(long finishID);
        Task<bool> CreateFinish(Finish finish);
        Task<bool> UpdateFinish(Finish finish);
        Task<bool> RemoveFinish(long finishID);
        Task<bool> EditPrice(long finishId, NewPriceDto newPriceDto);
        Task<bool> AddAnticipatedPrice(long finishId, NewPriceDto newPriceDto);
    }
}