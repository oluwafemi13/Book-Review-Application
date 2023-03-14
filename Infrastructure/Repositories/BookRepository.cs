using Application.Contract.Persistence.Interface;
//using Application.Features.Queries.GetBookList.GetBookByTitle;
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
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(DatabaseContext context): base(context) { }
        
        public async Task DeleteBook(string ISBN)
        {
            var book = await _dbContext.Books.Where(x=> x.ISBN== ISBN).FirstOrDefaultAsync();
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();

        }

        public async Task<Book> GetBookByISBN(string ISBN)
        {
            string clearedIn = ISBN.ToUpper().Replace("-", "").Replace(" ", "").Trim();

            var book = await _dbContext.Books.Where(x => x.ISBN == clearedIn).FirstOrDefaultAsync();
            return book;
        }

        public async Task<IEnumerable<RatingAverage>> GetBookByRatingAverage(decimal RatingAverage)
        {
            List<decimal> ratingList = new List<decimal>();
            decimal value = 0.1M;
            if(RatingAverage == Math.Round(RatingAverage, 0))
            {
                for (int i = 0; i < 5; i++)
                {
                    decimal newValue = Math.Round(RatingAverage, 1);
                    ratingList.Add(newValue);
                    newValue = RatingAverage - value;
                }
            }
            var bookIds=new List<RatingAverage>();
            foreach(var rating in ratingList)
            {
                bookIds = await _dbContext.RatingAverages.Where(x => x.AverageRating == Math.Round(RatingAverage, 1)).ToListAsync();
            }
            //var bookIds = await _dbContext.RatingAverages.Where(x => x.AverageRating == Math.Round(RatingAverage, 1)).ToListAsync();
            return bookIds;
        }

       
    }
}
