using Application.Contract.Persistence.Interface;
using Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Reviews.DeleteReview
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, bool>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ILogger<DeleteReviewCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteReviewCommandHandler(IReviewRepository reviewRepository, 
                                          ILogger<DeleteReviewCommandHandler> logger, 
                                          IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var search = await _reviewRepository.GetByGuidAsync(request.ReviewId);
            if (search == null)
            {
                _logger.LogInformation("Review DOes not Exist");
                return false;
                
            }
            await _reviewRepository.DeleteAsync(search);

            return true;
        }
    }
}
