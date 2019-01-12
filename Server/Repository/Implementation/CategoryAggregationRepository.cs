using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Model;
using Server.Repository.Base;
using Server.Repository.Interface;

namespace Server.Repository.Implementation
{
    public class CategoryAggregationRepository : RepositoryBase<CategoryAggregation>, ICategoryAggregationRepository
    {
        public CategoryAggregationRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        
        public async Task<IEnumerable<CategoryAggregation>> GetAllCategoriesAggregations()
        {
            return await FindAll();
        }

        public async Task<IEnumerable<CategoryAggregation>> GetCategoriesAggregationsById(long categoryID)
        {
            return await FindByCondition(cA => cA.RootCategoryId == categoryID);
        }
        
        public async Task<bool> CreateCategoryAggregation(CategoryAggregation categoryAggregation)
        {
            return await Create(categoryAggregation) && await Save();
        }

        public async Task<bool> RemoveCategoryFromCategoryAggregations(long categoryID)
        {
            var removedAll = true;
            var pAList = await FindByCondition(pA => pA.RootCategoryId == categoryID
                                                     || pA.SubcategoryId == categoryID);

            foreach (var item in pAList)
            {
                removedAll = removedAll && await Remove(item);
            }

            return removedAll;
        }
    }
}