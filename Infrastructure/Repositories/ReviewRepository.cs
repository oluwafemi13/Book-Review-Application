using Application.Contract.Persistence.Interface;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(DatabaseContext context): base(context)
        {

        }
        public IQueryable<Review> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetReviewByBook(string book)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetReviewByUsername(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<Review> FindUserByGuid(string UserId, Guid BookId)
        {
            var result = await _dbContext.Reviews.Where(x => x.user.Id == UserId)
                .Where(y => y.book.BookId == BookId)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
