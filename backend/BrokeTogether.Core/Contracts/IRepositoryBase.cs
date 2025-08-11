using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BrokeTogether.Core.Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        Task CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entities);
        void UpdateRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);


    }
}