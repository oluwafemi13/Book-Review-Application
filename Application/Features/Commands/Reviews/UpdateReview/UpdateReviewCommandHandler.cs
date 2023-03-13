using Application.Contract.Persistence.Interface;
using Application.Features.Commands.Reviews.DeleteReview;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Reviews.UpdateReview
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, bool>
    {

        private readonly IReviewRepository _reviewRepository;
        private readonly ILogger<UpdateReviewCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateReviewCommandHandler(IReviewRepository reviewRepository,
                                          ILogger<UpdateReviewCommandHandler> logger,
                                          IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var find = await _reviewRepository.GetByGuidAsync(request.ReviewId);
            if (find == null)
            {
                _logger.LogInformation("Review Not Found");
                return false;
            }
            find.ReviewTitle = request.ReviewTitle;
            find.review = request.review;

            await _reviewRepository.UpdateAsync(find);
            return true;
        }
    }
}
