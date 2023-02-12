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
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(DatabaseContext context): base(context) { }
        
        public async Task DeleteBookAndFormat(Guid Id)
        {
            var format = await _dbContext.Formats.Where(x=> x.BookId== Id).FirstOrDefaultAsync(); 
            var book = await _dbContext.Books.Where(x=> x.BookId== Id).FirstOrDefaultAsync();
            _dbContext.Formats.Remove(format);
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();

        }

        public async Task<Book> GetBookByISBN(string ISBN)
        {
            var book = await _dbContext.Books.Where(x => x.ISBN == ISBN).FirstOrDefaultAsync();
            return book;
        }

        public Task<IEnumerable<Book>> GetBookByRatingAverage(decimal RatingAverage)
        {
            throw new NotImplementedException();
        }
    }
}
