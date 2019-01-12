using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Model;

namespace Server.Repository.Interface
{
    public interface ICategoryAggregationRepository
    {
        Task<IEnumerable<CategoryAggregation>> GetAllCategoriesAggregations();
        Task<IEnumerable<CategoryAggregation>> GetCategoriesAggregationsById(long categoryID);
        Task<bool> CreateCategoryAggregation(CategoryAggregation categoryAggregation);
        Task<bool> RemoveCategoryFromCategoryAggregations(long categoryID);
    }
}