using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract.Persistence.Interface
{
    public interface IReviewRepository: IGenericRepository<Review>
    {
        IQueryable<Review> GetAll();
        Task<Review> GetReviewByUsername(string userName);
        Task<Review> GetReviewByBook(string book);
        Task<Review> FindUserByGuid(string UserId, Guid BookId);
        new Task DeleteAsync(Guid BookId);
    }
}
