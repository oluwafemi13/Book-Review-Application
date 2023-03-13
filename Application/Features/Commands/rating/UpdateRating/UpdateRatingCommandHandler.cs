using Application.Contract.Persistence.Interface;
using Application.Features.Commands.rating.DeleteRating;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.rating.UpdateRating
{
    public class UpdateRatingCommandHandler : IRequestHandler<UpdateRatingCommand, bool>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly ILogger<UpdateRatingCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateRatingCommandHandler(IRatingRepository ratingRepository,
                                        ILogger<UpdateRatingCommandHandler> logger,
                                        IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateRatingCommand request, CancellationToken cancellationToken)
        {
            var find = await _ratingRepository.GetByIdAsync(request.RatingId);
            if (find == null)
            {
                _logger.LogInformation("Rating Not Found");
                return false;
            }
            find.rating = request.rating;
           
            await _ratingRepository.UpdateAsync(find);
            return true;

        }
    }
}
