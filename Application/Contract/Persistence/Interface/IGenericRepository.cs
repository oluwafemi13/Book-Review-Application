using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract.Persistence.Interface
{
    public interface IGenericRepository<TContext> where TContext : class
    {
        Task<IReadOnlyList<TContext>> GetAllAsync();
        Task<IReadOnlyList<TContext>> GetAsync();
        
        Task<TContext> GetByIdAsync(int id);
        Task<TContext> GetByGuidAsync(Guid id);
        Task<TContext> GetByStringIdAsync(string Id);
        Task<TContext> GetByNameAsync(string Name);
        Task<TContext> AddAsync(TContext entity);
        Task UpdateAsync(TContext entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(TContext entity);



    }
}
