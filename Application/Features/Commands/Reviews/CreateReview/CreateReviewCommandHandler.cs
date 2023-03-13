using Application.Contract.Persistence.Interface;
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

namespace Application.Features.Commands.Reviews.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Response>
    {

        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateReviewCommandHandler> _logger;

        public CreateReviewCommandHandler(IReviewRepository reviewRepository, 
                                           IMapper mapper, 
                                           ILogger<CreateReviewCommandHandler> logger)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var result = await _reviewRepository.FindUserByGuid(request.userId, request.bookId);

            if (result != null)
            {
                return new Response
                {
                    Status = "Error",
                    Message = $"Rating for user {request.userId} on book with Id {request.bookId} already exist",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            var rating = new Review()
            {
                
                ReviewTitle = request.ReviewTitle,
                review = request.review,
                book = new Book { BookId = request.bookId },
                user = new User { Id = request.userId }
            };
            await _reviewRepository.AddAsync(rating);

            return new Response
            {
                Status = "Success",
                Message = "Successfully Created",
                StatusCode = StatusCodes.Status201Created
            };
        }
    }
}
