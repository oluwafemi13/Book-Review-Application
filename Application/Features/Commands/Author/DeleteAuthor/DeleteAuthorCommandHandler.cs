using Application.Contract.Persistence.Interface;
using Application.Exceptions;
using Application.Features.Commands.author.CreateAuthor;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.author.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
    {

        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<DeleteAuthorCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository,
                                        ILogger<DeleteAuthorCommandHandler> logger,
                                        IMapper mapper)
        {
            _authorRepository = authorRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
           var runCheck = await _authorRepository.GetByGuidAsync(request.AuthorId);
            if (runCheck == null)
            {
                _logger.LogError($"user with user id {request.AuthorId} was not found");
                //throw new NotFoundException(nameof(request.AuthorId));

            }
            await _authorRepository.DeleteByGuidAsync(request.AuthorId);
            return Unit.Value;
        }
    }
}
