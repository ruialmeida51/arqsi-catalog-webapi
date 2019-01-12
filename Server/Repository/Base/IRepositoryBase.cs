using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Server.Repository.Base
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> FindAll();
        Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task<bool> Create(T entity);
        Task<bool> Remove(T entity);
        Task<bool> Update(T entity);
        Task<bool> Save();
    }
}