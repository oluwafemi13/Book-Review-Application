﻿using Application.Contract.Persistence.Interface;
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
    public class RatingAverageRepository : GenericRepository<RatingAverage>, IRatingAverageRepository
    {
        public RatingAverageRepository(DatabaseContext db):base(db) { }
       
        public Task<IEnumerable<Rating>> GetRatingAverageByBook(string book)
        {
            throw new NotImplementedException();
        }

        public new async Task DeleteAsync(Guid BookId)
        {
            var result = await _dbContext.RatingAverages.Where(x => x.book.BookId == BookId).ToListAsync();

            _dbContext.RatingAverages.RemoveRange(result);
            await _dbContext.SaveChangesAsync();


        }
    }
}
