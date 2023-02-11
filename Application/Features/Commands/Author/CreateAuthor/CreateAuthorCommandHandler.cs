using Application.Contract.Persistence.Interface;
using Application.Exceptions;
using Domain.Entities;
using Application.Features.Commands.Reviews.DeleteReview;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.author.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<CreateAuthorCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateAuthorCommandHandler(IAuthorRepository authorRepository, 
                                        ILogger<CreateAuthorCommandHandler> logger, 
                                        IMapper mapper)
        {
            _authorRepository = authorRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var runCheck =await _authorRepository.GetByNameAsync(request.AuthorName);
            if(runCheck is null)
            {
                _logger.LogInformation($"Author {request.AuthorName} was not found");
                throw new NotFoundException(nameof(request.AuthorName));
                
            }
            var map = _mapper.Map<Author>(request);
            await _authorRepository.AddAsync(map);

            return request.AuthorId;
        }
    }
}
