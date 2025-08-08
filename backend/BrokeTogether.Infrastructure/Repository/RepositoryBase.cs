using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BrokeTogether.Core.Contracts;
using BrokeTogether.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BrokeTogether.Infrastructure.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        private readonly RepositoryContext _repositoryContext;
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public async Task CreateAsync(T entity)
        {
            await _repositoryContext.Set<T>().AddAsync(entity);

        }

        public Task DeleteAsync(T entity)
        {
            _repositoryContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return trackChanges
                ? _repositoryContext.Set<T>()
                : _repositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges
                ? _repositoryContext.Set<T>().Where(expression).AsNoTracking()
                : _repositoryContext.Set<T>().Where(expression);
        }

        public Task UpdateAsync(T entity)
        {
            _repositoryContext.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

    }
}