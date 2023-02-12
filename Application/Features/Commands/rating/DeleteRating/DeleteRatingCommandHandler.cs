using Application.Contract.Persistence.Interface;
using Application.Exceptions;
using Application.Features.Commands.rating.CreateRating;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.rating.DeleteRating
{
    public class DeleteRatingCommandHandler : IRequestHandler<DeleteRatingCommand, bool>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly ILogger<DeleteRatingCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteRatingCommandHandler(IRatingRepository ratingRepository,
                                        ILogger<DeleteRatingCommandHandler> logger,
                                        IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
        {
            var find = await _ratingRepository.GetByIdAsync(request.RatingId);
            if (find == null)
                _logger.LogInformation($"Rating with Id{request.RatingId} not found");
            await _ratingRepository.DeleteAsync(request.RatingId);
            return true;
        }
    }
}
