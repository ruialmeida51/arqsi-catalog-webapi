using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Model;
using Server.Repository.Base;
using Server.Repository.Interface;

namespace Server.Repository.Implementation
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return (await FindAll()).OrderBy(category => category.Name);
        }

        public async Task<Category> GetCategoryById(long categoryID)
        {
            return (await FindByCondition(category => category.Id.Equals(categoryID))).DefaultIfEmpty(null)
                .FirstOrDefault();
        }

        public async Task<bool> CreateCategory(Category category)
        {
            await Create(category);
            return await Save();
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            return await Update(category);
        }

        public async Task<bool> RemoveCategory(long categoryID)
        {
            var category = await GetCategoryById(categoryID);
            return await Remove(category);
        }
    }
}