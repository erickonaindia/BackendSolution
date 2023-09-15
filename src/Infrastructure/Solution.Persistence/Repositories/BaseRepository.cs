using Microsoft.EntityFrameworkCore;
using Solution.Application.Contracts.Persistence;
using Solution.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid Id)
        {
            return await _dbContext.Set<T>().FindAsync(Id);
        }
        public async Task<IQueryable<T>> GetQueryAsync(Expression<Func<T, bool>> predicate)
        {
            return (await GetQueryAsync()).Where(predicate);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);

            return entity;
        }

        public async Task<T> SingleAsync(Expression<Func<T, bool>> criteria)
        {
            return (await GetQueryAsync()).Single(criteria);
        }
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
        public async Task<IQueryable<T>> GetQueryAsync()
        {
            IQueryable<T> query = _dbContext.Set<T>();
            return query;
        }
        public async Task<IQueryable<TEntity>> GetQueryAsync<TEntity>() where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsNoTracking();
            return query;
        }
    }
}
