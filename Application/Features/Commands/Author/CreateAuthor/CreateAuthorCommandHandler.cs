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

namespace Application.Features.Commands.Author.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ILogger<CreateAuthorCommand> _logger;
        private readonly IMapper _mapper;
        public Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
