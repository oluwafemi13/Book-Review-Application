using Application.Contract.Persistence.Interface;
using Application.Features.Commands.author.CreateAuthor;
using Application.ISBN_Validation;
using Application.Model;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Features.Commands.book.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Response>
    {
        private readonly IBookRepository _BookRepository;
        private readonly IFormatRepository _formatRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookGenreRepository _BookGenreRepository;
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly ILogger<CreateBookCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateBookCommandHandler(IBookRepository BookRepository,
                                        ILogger<CreateBookCommandHandler> logger,
                                        IBookGenreRepository BookGenreRepository,
                                        IBookAuthorRepository bookAuthorRepository,
                                        IAuthorRepository authorRepository,
                                        IGenreRepository genreRepository,
                                        IMapper mapper,
                                        IFormatRepository formatRepository)
        {
            _formatRepository= formatRepository;
            _BookRepository = BookRepository;
            _genreRepository= genreRepository;
            _BookGenreRepository = BookGenreRepository;
            _authorRepository = authorRepository;
            _bookAuthorRepository = bookAuthorRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
            var findBook = await _BookRepository.GetBookByISBN(request.ISBN);
            var findAuthor = await _authorRepository.GetAuthorById(request.AuthorId);

            if(findAuthor == null)
            {
                    _logger.LogInformation("Author does not Exist");
                    return new Response
                    {
                        Status = "Error",
                        Message = $"Author with Id {request.AuthorId} does not Exist",
                        StatusCode = StatusCodes.Status400BadRequest
                    };
            }

            if (findBook != null)
            {
                    _logger.LogError($"Book Already Exist");
                return new Response
                {
                    Status = "Error",
                    Message = $"Book with ISBN Number {request.ISBN} already Exist",
                    StatusCode = StatusCodes.Status400BadRequest
                };
              
            }

            //validate ISBN 10 digits number
            string clearedIn = request.ISBN.ToUpper().Replace("-", "").Replace(" ", "").Trim();  
            var validation = new ISBN10Validation();
            bool valid = validation.validateISBN10(request.ISBN);
            if (valid == false)
            {
                _logger.LogInformation("Invalid ISBN Number");
                return new Response
                {
                    Status = "Error",
                    Message = "Invalid ISBN Number",
                    
                };
                
            }
            
                    var book = new Book();
                    var format = new Format();
                    book.ISBN = clearedIn;
                    book.BookId = Guid.NewGuid();
                    book.CoverImage = request.CoverImage;
                    book.Description = request.Description;
                    book.Language = request.Language;
                    book.BookTitle = request.BookTitle;
                    book.Summary = request.Summary;
                    book.DatePublished = request.DatePublished;
                    book.AuthorId = request.AuthorId;
                book.format.NumberOfPages = request.format.NumberOfPages;
                book.format.FormatType = request.format.FormatType;
                book.format.BookId = book.BookId;




                var create = await _BookRepository.AddAsync(book);
                
                
            /*var format = new Format();
            format.FormatType = request.FormatType;
            format.NumberOfPages = request.NumberOfPages;
            format.BookId = book.BookId;
            await _formatRepository.AddAsync(format);*/

                //GENRE
                foreach (var genre in request.GenreName)
                {
                    var findGenre = await _genreRepository.FindGenreByName(genre);
                    if (findGenre == null)
                    {
                        var gen = new Genre();
                        gen.GenreName = genre;
                        gen.DateCreated = DateTime.Now;

                        var result = await _genreRepository.AddAsync(gen);
                        var bookgenre = new BookGenre();
                        bookgenre.GenreId = result.GenreId;
                        bookgenre.BookId = book.BookId;

                        await _BookGenreRepository.CreateBookGenre(bookgenre);

                    }
                    else
                    {
                        var bookgenre = new BookGenre();
                        bookgenre.Id++;
                        bookgenre.GenreId = findGenre.GenreId;
                        bookgenre.BookId = book.BookId;
                        await _BookGenreRepository.CreateBookGenre(bookgenre);
                    }

                }

                //Author
                var bookauthor = new BookAuthor();
                bookauthor.Id++;
                bookauthor.AuthorId = book.AuthorId;
                bookauthor.BookId = book.BookId;
                await _bookAuthorRepository.CreateBookAuthor(bookauthor);

                return new Response
            {
                Status = "Success",
                Message = "Successfully Created",
                StatusCode = StatusCodes.Status201Created
            };
            }
            catch (Exception)
            {

                throw;
            }
        }



        private bool validateISBN(string ISBN)
        {
            List<int> list = new List<int>();
            int total = 0;
            string converted = "";
            if (ISBN.Length == 10)
            {
                converted = convert10to13(ISBN);

            }
            char[] ch = converted.ToCharArray();
            for (int i = 0; i < ch.Length; i++)
            {
                list.Add(Convert.ToInt32(ch[i]));
            }
            foreach (int i in list)
            {
                total += i;
            }
            if (total % 10 == 0)
                return true;
            return false;
        }

        private string convert10to13(string ISBN)
        {
            int count = 1;
            int weight1 = 1;
            int weight2 = 3;
            string prefix = "978";
            List<string> list = new List<string>();
            List<int> list2 = new List<int>();
            List<int> list3 = new List<int>();
            StringBuilder builder = new StringBuilder();
            string clearedIn = ISBN.ToUpper().Replace("-", "").Replace(" ", "").Trim();
            string removeLastcharacter = clearedIn.Remove(9);
            list.Add(prefix + removeLastcharacter);
            for (int i = 0; i < list.Count; i++)
            {
                list2.Add(Convert.ToInt32(list[i]));
                list3.Add(Convert.ToInt32(list[i]));
            }

            //calculation to get the last digit
            for (var j = 1; j <= list2.Count; j++)
            {
                if (j % 2 == 0)
                {
                    list2[j] = list2[j] * 3;
                }

            }

            var sum = Sum(list2);
            int checkDigit = 10 - sum % 10;
            list3.Add(checkDigit);

            foreach (var i in list3)
                builder.Append(i);

            return builder.ToString();

        }

        private bool validateISBN10(string ISBN)
        {
            string clearedIn = ISBN.ToUpper().Replace("-", "").Replace(" ", "").Trim();

            //Eingabe nach int[] parsen
            int[] numbers = clearedIn.ToCharArray().Select(i => i == 'X' ? 10 : int.Parse(i.ToString())).ToArray();

            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += numbers[i] * (10 - i);
            }

            if (sum % 11 == 0)
            {
                return true;
            }
            return false;
        }

        private int Sum(List<int> list)
        {
            int result = 0;
            for (var i = 0; i < list.Count; i++)
            {
                result += list[i];
            }
            return result;
        }
    }
}
