using Application.Contract.Persistence.Interface;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Reviews.CreateReviewCommand
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, string>
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

        public async Task<string> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var confirmifReviewExists = _reviewRepository.GetByStringIdAsync(request.ReviewId.ToString());
            if(confirmifReviewExists != null)
            {
                _logger.LogInformation($"Review ALready Exists....You can not create a duplicate review");
            }
           var mapped =  _mapper.Map<Review>(request);
            await _reviewRepository.AddAsync(mapped);
            return request.ReviewId.ToString(); 
        }
    }
}
