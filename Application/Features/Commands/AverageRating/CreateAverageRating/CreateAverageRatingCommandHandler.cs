using Application.Contract.Persistence.Interface;
using Application.Model;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.AverageRating.CreateAverageRating
{
    public class CreateAverageRatingCommandHandler : IRequestHandler<CreateAverageRatingCommand, Response>
    {
        private readonly IRatingRepository _ratingRepo;
        private readonly ILogger<CreateAverageRatingCommandHandler> _logger;
        private readonly IRatingAverageRepository _ratingAverageRepo;

        public CreateAverageRatingCommandHandler(IRatingRepository ratingRepo, 
                                                ILogger<CreateAverageRatingCommandHandler> logger,
                                                IRatingAverageRepository ratingAverageRepo)
        {
            _ratingRepo = ratingRepo;
            _logger = logger;
            _ratingAverageRepo = ratingAverageRepo;
        }

        public async Task<Response> Handle(CreateAverageRatingCommand request, CancellationToken cancellationToken)
        {
            var fetchRatings = await _ratingRepo.GetRatingsByBookId(request.BookId);
            if(fetchRatings == null)
            {
                _logger.LogInformation($"Book with Id {request.BookId} has no rating from any user");
                return new Response
                {
                    Status = "Error",
                    Message= "Book with Id {request.BookId} has no rating from any user",
                    StatusCode= StatusCodes.Status404NotFound
                };
            }
            var totalCount = 0;
            decimal total = 0;
            decimal averageRating=0;
            foreach(var rating in fetchRatings)
            {
                total = total + rating.rating;
                totalCount++;
            }
            averageRating =Math.Round(total / totalCount, 1) ;
            var ratingAvg = new RatingAverage();
            ratingAvg.AverageRating = averageRating;
            ratingAvg.BookId= request.BookId;

            var result = _ratingAverageRepo.AddAsync(ratingAvg);
            return new Response
            {
                Status = "Success",
                Message = "Successfully Created",
                StatusCode = StatusCodes.Status201Created
            };

        }
    }
}
