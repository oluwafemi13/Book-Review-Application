using Application.Contract.Persistence.Interface;
using Application.Features.Commands.genre.DeleteGenre;
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

namespace Application.Features.Commands.rating.CreateRating
{
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, Response>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly ILogger<CreateRatingCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateRatingCommandHandler(IRatingRepository ratingRepository,
                                        ILogger<CreateRatingCommandHandler> logger,
                                        IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            var result = await _ratingRepository.FindUserByGuid(request.userId, request.BookId);

            if(result != null)
            {
                return new Response
                {
                    Status="Error",
                    Message=$"Rating for user {request.userId} on book with Id {request.BookId} already exist",
                    StatusCode=StatusCodes.Status400BadRequest
                };
            }
            var rating = new Rating()
            {
                rating = request.rating,
                book = new Book{BookId = request.BookId},
                user = new User{Id = request.userId}
            };
            await _ratingRepository.AddAsync(rating);

            return new Response
            {
                Status="Success",
                Message = "Successfully Created",
                StatusCode=StatusCodes.Status201Created
            };
           
            
           
        }
    }
}
