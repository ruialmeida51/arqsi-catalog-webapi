using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Model;

namespace Server.Repository.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        protected RepositoryContext RepositoryContext { get; set; }

        public async Task<IEnumerable<T>> FindAll()
        {
            return await RepositoryContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return await RepositoryContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<bool> Create(T entity)
        {
            try
            {
                await RepositoryContext.Set<T>().AddAsync(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                RepositoryContext.Set<T>().Update(entity);
                return await Save();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Remove(T entity)
        {
            try
            {
                RepositoryContext.Set<T>().Remove(entity);
                return await Save();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Save()
        {
            try
            {
                await RepositoryContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}