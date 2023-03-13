using Application.Contract.Persistence.Interface;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<TContext>: IGenericRepository<TContext> where TContext:class
    {
        protected readonly DatabaseContext _dbContext;

        public GenericRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<TContext>> GetAllAsync()
        {
            return await _dbContext.Set<TContext>().ToListAsync();
        }
        

        public async Task<IReadOnlyList<TContext>> GetAsync(Expression<Func<TContext, bool>> predicate)
        {
            return await _dbContext.Set<TContext>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<TContext>> GetAsync(Expression<Func<TContext, bool>> predicate = null, Func<IQueryable<TContext>, IOrderedQueryable<TContext>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<TContext> query = _dbContext.Set<TContext>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<TContext>> GetAsync(Expression<Func<TContext, bool>> predicate = null, Func<IQueryable<TContext>, IOrderedQueryable<TContext>> orderBy = null, List<Expression<Func<TContext, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<TContext> query = _dbContext.Set<TContext>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public virtual async Task<TContext> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TContext>().FindAsync(id);
        }

        public virtual async Task<TContext> GetByGuidAsync(Guid id)
        {
            return await _dbContext.Set<TContext>().FindAsync(id);
        }
        public async Task<TContext> GetByNameAsync(string Name)
        {
            return await _dbContext.Set<TContext>().FindAsync(Name);
        }
        public async Task<TContext> AddAsync(TContext entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(TContext entity)
        {
           
            _dbContext.Set<TContext>().Update(entity);
            await _dbContext.SaveChangesAsync();
            
        }
        
        public async Task<bool> DeleteAsync(TContext entity)
        {
           var result =  _dbContext.Set<TContext>().Remove(entity);
            if(result.State== EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeleteAsync(Guid id)
        {
            _dbContext.Entry(id).State= EntityState.Deleted;
            _dbContext.SaveChangesAsync();
        }

    }
}
