using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Model;
using Server.Repository.Base;
using Server.Repository.Interface;

namespace Server.Repository.Implementation
{
    public class AestheticLineRepository : RepositoryBase<AestheticLine>, IAestheticLineRepository
    {
        public AestheticLineRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<AestheticLine>> GetAllAestheticLines()
        {
            return (await FindAll()).OrderBy(aestheticLine => aestheticLine.Name);
        }

        public async Task<AestheticLine> GetAestheticLineById(long aestheticLineID)
        {
            return (await FindByCondition(aestheticLine => aestheticLine.Id.Equals(aestheticLineID)))
                .DefaultIfEmpty(null).FirstOrDefault();
        }

        public async Task<bool> CreateAestheticLine(AestheticLine aestheticLine)
        {
            await Create(aestheticLine);
            return await Save();
        }

        public async Task<bool> UpdateAestheticLine(AestheticLine aestheticLine)
        {
            return await Update(aestheticLine);
        }

        public async Task<bool> RemoveAestheticLine(long aestheticLineID)
        {
            var aestheticLine = await GetAestheticLineById(aestheticLineID);
            return await Remove(aestheticLine);
        }
    }
}