using Application.Contract.Persistence.Interface;
using Application.DTO;
using Application.Features.Queries.GetBookList;
//using Application.Features.Queries.GetBookList.GetBookByTitle;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

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

        public async Task<IReadOnlyList<Book>> GetBook(int AuthorId)
        {
            return await _dbContext.Books.Where(x => x.AuthorId == AuthorId).ToListAsync();
        }

        public async Task<Book> GetBookByISBN(string ISBN)
        {
            string clearedIn = ISBN.ToUpper().Replace("-", "").Replace(" ", "").Trim();

            var book = await _dbContext.Books.Where(x => x.ISBN == clearedIn).FirstOrDefaultAsync();
            return book;
        }


        public async Task<IEnumerable<BookVM>> GetByName(string Name)
        {
            var list = new List<BookVM>();
            var genreList = new List<string>();
            var newBooks = new BookVM();
            var book =await _dbContext.Books.Where(x=> x.BookTitle == Name).ToListAsync();
            foreach(var item in book)
            {
                var bookGenre = await _dbContext.BookGenres.Where(x => x.BookId == item.BookId).Select(gi => gi.GenreId).ToListAsync();
                var author = await _dbContext.Authors.Where(x => x.AuthorId == item.AuthorId).FirstOrDefaultAsync();
                var rating = await _dbContext.RatingAverages.Where(x => x.BookId == item.BookId).FirstOrDefaultAsync();
                

                for(int i=0; i<bookGenre.Count; i++)
                {
                    var genres = await _dbContext.Genres.Where(x => x.GenreId == bookGenre[i]).Select(gn => gn.GenreName).FirstOrDefaultAsync();
                    genreList.Add(genres);
                }
                newBooks.ISBN = item.ISBN;
                newBooks.Author = author.AuthorName;
                newBooks.Summary = item.Summary;
                newBooks.DatePublished = item.DatePublished;
                newBooks.BookId = item.BookId;
                newBooks.CoverImage = item.CoverImage;
                newBooks.BookTitle = item.BookTitle;
                newBooks.Description = item.Description;
                newBooks.Language = item.Language;
                newBooks.AverageRating = rating.AverageRating;
                newBooks.Genres = new List<string>(genreList);

                list.Add(newBooks);
                
            }
            
            return list;
        }

        public async Task<IEnumerable<BookVM>> GetBookByRatingAverage(decimal RatingAverage)
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
            var books = new List<BookVM>();
            var newBooks = new BookVM();
            var ratings = await _dbContext.RatingAverages.Where(x => x.AverageRating == Math.Round(RatingAverage, 1)).ToListAsync();
            if(ratings == null)
            {
                return new List<BookVM>();
            }

            foreach (var book in ratings)
            {
                bookList= await _dbContext.Books.Where(x=> x.BookId== book.BookId).ToListAsync();
               
            }

            for (int i = 0; i < bookList.Count; i++)
            {
                var result = await _dbContext.Authors.FindAsync(bookList[i].AuthorId);
                //var result2= await _dbContext.BookGenres.Where(x => x.BookId == bookList[i].BookId).Select(x=> x.Genre).ToListAsync();
                newBooks.BookId = bookList[i].BookId;
                newBooks.Author = result.AuthorName;
                newBooks.ISBN = bookList[i].ISBN;
                newBooks.BookTitle = bookList[i].BookTitle;
                newBooks.DatePublished= bookList[i].DatePublished;
                newBooks.Summary = bookList[i].Summary;
                newBooks.Description=bookList[i].Description;
                newBooks.Language = bookList[i].Language;
                newBooks.CoverImage = bookList[i].CoverImage;
                newBooks.AverageRating = RatingAverage;
                
                books.Add(newBooks);

            }
            //var bookIds = await _dbContext.RatingAverages.Where(x => x.AverageRating == Math.Round(RatingAverage, 1)).ToListAsync();
            return books;
        }

       
    }
}
