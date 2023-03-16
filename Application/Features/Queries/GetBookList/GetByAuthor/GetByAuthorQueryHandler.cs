using Application.Contract.Persistence.Interface;
using Application.DTO;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookList.GetByAuthor
{
    public class GetByAuthorQueryHandler : IRequestHandler<GetByAuthorQuery, IEnumerable<BookVM>>
    {
        private readonly IAuthorRepository _authorRepo;
        private readonly IBookRepository _bookrRepo;
        private readonly IRatingAverageRepository _RA;
        private readonly IGenreRepository _GR;
        private readonly IBookGenreRepository _BG;
        private readonly ILogger<GetByAuthorQueryHandler> _logger;

        public GetByAuthorQueryHandler(IAuthorRepository authorRepo, 
                                        IBookRepository bookrRepo, 
                                        ILogger<GetByAuthorQueryHandler> logger,
                                        IRatingAverageRepository RA,
                                        IGenreRepository GR,
                                        IBookGenreRepository BG)
        {
            _authorRepo = authorRepo;
            _bookrRepo = bookrRepo;
            _logger = logger;
            _RA = RA;
            _GR = GR;
            _BG = BG;
        }

        public async Task<IEnumerable<BookVM>> Handle(GetByAuthorQuery request, CancellationToken cancellationToken)
        {
            var list = new List<BookVM>();
            var book = new BookVM();
            var result = await _authorRepo.GetAsync(x => x.AuthorName == request.Author);
            
            foreach (var item in result)
            {
                var books = await _bookrRepo.GetBook(item.AuthorId);
                foreach(var i in books)
                {
                    //average Rating
                    var rating = await _RA.GetRatingAverageByBook(i.BookId);
                    //Genre
                    var genre = await _BG.GetAsync(x => x.BookId == book.BookId);
                    var genreList = new List<string>();
                    foreach (var gen in genre)
                    {
                        var genreName = await _GR.GetById(gen.GenreId);
                        genreList.Add(genreName);
                    }
                    book.ISBN = i.ISBN;
                    book.Author = item.AuthorName;
                    book.DatePublished = i.DatePublished;
                    book.AverageRating = rating;
                    book.BookTitle = i.BookTitle;
                    book.BookId = i.BookId;
                    book.CoverImage = i.CoverImage;
                    book.Description = i.Description;
                    book.Summary = i.Summary;
                    book.Genres = new List<string>(genreList);
                    book.Language = i.Language;
                    list.Add(book);
                }
                
                
            }

            return list;
            
            
        }
    }
}
