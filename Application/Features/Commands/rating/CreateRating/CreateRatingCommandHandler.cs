using Application.Contract.Persistence.Interface;
using Application.Features.Commands.genre.DeleteGenre;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.rating.CreateRating
{
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, int>
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
        public async Task<int> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<Rating>(request);
            await _ratingRepository.AddAsync(map);
            return map.RatingId;
            
           
        }
    }
}
