using Application.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Application.Contract.Persistence.Interface
{
    public interface IGenericRepository<TContext> where TContext : class
    {
        Task<IReadOnlyList<TContext>> GetAllAsync();
        Task<IPagedList<TContext>> GetAllPagedAsync(RequestParameters requestParams);
        Task<IReadOnlyList<TContext>> GetAsync(Expression<Func<TContext, bool>> predicate);
        Task<IReadOnlyList<TContext>> GetAsync(Expression<Func<TContext, bool>>? predicate = null,
                                        Func<IQueryable<TContext>, IOrderedQueryable<TContext>>? orderBy = null,
                                        string? includeString = null,
                                        bool disableTracking = true);
        Task<IReadOnlyList<TContext>> GetAsync(Expression<Func<TContext, bool>>? predicate = null,
                                       Func<IQueryable<TContext>, IOrderedQueryable<TContext>>? orderBy = null,
                                       List<Expression<Func<TContext, object>>>? includes = null,
                                       bool disableTracking = true);

        Task<TContext> GetByIdAsync(int id);
        Task<TContext> GetByGuidAsync(Guid id);
        //Task<TContext> GetByStringIdAsync(string Id);
        Task<TContext> GetByNameAsync(string Name);
        Task<TContext> AddAsync(TContext entity);
        Task UpdateAsync(TContext entity);
        Task DeleteAsync(Guid id);
        Task<bool> DeleteAsync(TContext entity);
        //Task DeleteByGuidAsync(Guid id);



    }
}
