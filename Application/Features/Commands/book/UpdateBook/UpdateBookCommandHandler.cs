using Application.Contract.Persistence.Interface;
using Application.Exceptions;
using Application.Features.Commands.book.CreateBook;
using Application.ISBN_Validation;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.book.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Response>
    {
        private readonly IBookRepository _BookRepository;
        private readonly IFormatRepository _formatRepository;
        private readonly ILogger<UpdateBookCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IBookRepository BookRepository,
                                        ILogger<UpdateBookCommandHandler> logger,
                                        IFormatRepository formatRepository,
                                        IMapper mapper)
        {
            _BookRepository = BookRepository;
            _formatRepository = formatRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var search = await _BookRepository.GetByGuidAsync(request.BookId);
                var findFormat = await _formatRepository.FindFormat(request.BookId);
                if (search == null)
                {
                    _logger.LogError($"Book with Id {request.BookId} not found");
                    return new Response
                    {
                        Status = "Error",
                        Message = "Book Does not Exist",
                        StatusCode=StatusCodes.Status404NotFound
                    };
                }
                var validate = new ISBN10Validation();
                var CheckISBN = await _BookRepository.GetBookByISBN(request.ISBN);
                if(CheckISBN != null)
                {
                    return new Response
                    {
                        Status = "Error",
                        Message = "Duplicate ISBN number",
                        StatusCode = StatusCodes.Status409Conflict
                    };
                }
                var validateISBN = validate.validateISBN10(request.ISBN);
                if(validateISBN != true)
                {
                    search.BookTitle = request.BookTitle;
                    //search.AuthorId = request.AuthorId;
                    search.CoverImage ??= request.CoverImage;
                    search.Language = request.Language;
                    search.DatePublished = request.DatePublished;
                    search.Description = request.Description;
                    search.Summary = request.Summary;
                    await _BookRepository.UpdateAsync(search);

                }
                else
                {
                    search.BookTitle = request.BookTitle;
                    //search.AuthorId = request.AuthorId;
                    search.CoverImage ??= request.CoverImage;
                    search.Language = request.Language;
                    search.ISBN = request.ISBN;
                    search.DatePublished = request.DatePublished;
                    search.Description = request.Description;
                    search.Summary = request.Summary;
                    await _BookRepository.UpdateAsync(search);
                }
                
                if (findFormat== null)
                {
                    _logger.LogInformation("format does not Exist");
                    return new Response
                    {
                        Status = "Error",
                        Message = "Format does not Exist, Book Successfully Updated",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                findFormat.FormatType = request.FormatType;
                findFormat.NumberOfPages = request.NumberOfPages;
                await _formatRepository.UpdateAsync(findFormat);

                return new Response
                {
                    Status= "Success",
                    Message = "Successfully Updated",
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        
    }
}
