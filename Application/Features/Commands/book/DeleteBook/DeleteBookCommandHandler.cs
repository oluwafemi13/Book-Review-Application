using Application.Contract.Persistence.Interface;
using Application.Features.Commands.book.CreateBook;
using Application.Model;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.book.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Response>
    {
        private readonly IBookRepository _BookRepository;
        //private readonly IFormatRepository _formatRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IRatingAverageRepository _ratingAvgRepository;
        private readonly ILogger<DeleteBookCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteBookCommandHandler(IBookRepository BookRepository,
                                        ILogger<DeleteBookCommandHandler> logger,
                                        IRatingRepository ratingRepository,
                                        IReviewRepository reviewRepository,
                                        IRatingAverageRepository ratingAverage,
                                        IMapper mapper)
        {
            
            _BookRepository = BookRepository;
            _ratingAvgRepository = ratingAverage;
            _ratingRepository = ratingRepository;
            _reviewRepository= reviewRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var search = await _BookRepository.GetBookByISBN(request.ISBN);
            if (search == null)
            {
                _logger.LogInformation($"Book {request.ISBN}not found");
                return new Response
                {
                    Status = "Error",
                    Message = "Book Not Found",
                    StatusCode = StatusCodes.Status404NotFound
                };

            }
            await _BookRepository.DeleteBook(request.ISBN);
            await _reviewRepository.DeleteAsync(search.BookId);
            await _ratingAvgRepository.DeleteAsync(search.BookId);
            await _ratingRepository.DeleteAsync(search.BookId);
            return new Response
            {
                Status = "Success",
                Message = "Successfully Deleted",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
