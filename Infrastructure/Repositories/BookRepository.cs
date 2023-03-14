using Application.Contract.Persistence.Interface;
using Application.Features.Queries.GetBookList;
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
        public async Task<IEnumerable<Book>> GetByName(string Name)
        {
            var book =await _dbContext.Books.Where(x=> x.BookTitle == Name).ToListAsync();
            return book;
        }

        public async Task<IEnumerable<Book>> GetBookByRatingAverage(decimal RatingAverage)
        {
            /*var ratingList = new List<decimal>();
            decimal value = 0.1M;
            ratingList.Add(RatingAverage);
             for (int i = 0; i < 5; i++)
             {
                    //decimal newValue = Math.Round(RatingAverage, 1);
                    RatingAverage = RatingAverage - value;
                    ratingList.Add(RatingAverage);
            }*/
            
            //var ratings=new List<RatingAverage>();
            var bookList = new List<Book>();
            var ratings = await _dbContext.RatingAverages.Where(x => x.AverageRating == Math.Round(RatingAverage, 1)).ToListAsync();
            foreach (var book in ratings)
            {
                bookList= await _dbContext.Books.Where(x=> x.BookId== book.BookId).ToListAsync();
            }
            //var bookIds = await _dbContext.RatingAverages.Where(x => x.AverageRating == Math.Round(RatingAverage, 1)).ToListAsync();
            return bookList;
        }

       
    }
}
