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
    public class RatingRepository: GenericRepository<Rating>, IRatingRepository
    {

        public RatingRepository(DatabaseContext db) : base(db)
        {

        }

        public async Task<Rating> FindUserByGuid(string UserId, Guid BookId)
        {
            var result = await _dbContext.Ratings.Where(x => x.user.Id == UserId)
                .Where(y=> y.book.BookId == BookId)
                .FirstOrDefaultAsync();
                
            return result;
        }

        public async Task<IEnumerable<Rating>> GetRatingsByBookId(Guid BookId)
        {
            var result = await _dbContext.Ratings
                //.Include(y=> y.rating)
                .Where(x=> x.book.BookId == BookId)
                .ToListAsync();

            return result;
        }

        public new async Task DeleteAsync(Guid BookId)
        {
            var result = await _dbContext.Ratings.Where(x => x.book.BookId == BookId).ToListAsync();

            _dbContext.Ratings.RemoveRange(result);
            await _dbContext.SaveChangesAsync();


        }
    }
}
