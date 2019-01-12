using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.DTO;
using Server.Model;
using Server.Repository.Base;
using Server.Repository.Interface;

namespace Server.Repository.Implementation
{
    public class FinishRepository : RepositoryBase<Finish>, IFinishRepository
    {
        public FinishRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Finish>> GetAllFinishes()
        {
            return (await FindAll()).OrderBy(finish => finish.Name).ToList();
        }

        public  async Task<Finish> GetFinishById(long finishID)
        {
            return (await FindByCondition(finish => finish.Id.Equals(finishID))).DefaultIfEmpty(null).FirstOrDefault();
        }

        public  async Task<bool> CreateFinish(Finish finish)
        {
            await Create(finish);
            return await Save();
        }

        public  async Task<bool> UpdateFinish(Finish finish)
        {
            return await Update(finish);
        }

        public  async Task<bool> RemoveFinish(long finishID)
        {
            var finish = await GetFinishById(finishID);
            return await Remove(finish);
        }
        
        public async Task<bool> AddAnticipatedPrice(long finishId, NewPriceDto newPriceDto)
        {
            var finish = await GetFinishById(finishId);
            if (finish == null) return false;

            var res = finish.PricePSM.AddAnticipatedPrice(newPriceDto.Price, newPriceDto.Timestamp);
            if (!res) return false;

            return await UpdateFinish(finish);
        }
        
        public async Task<bool> EditPrice(long finishId, NewPriceDto newPriceDto)
        {
            var finish = await GetFinishById(finishId);
            if (finish == null) return false;

            var res = finish.PricePSM.EditPrice(newPriceDto.Price, newPriceDto.Timestamp);
            if (!res) return false;

            return await UpdateFinish(finish);
        }
    }
}