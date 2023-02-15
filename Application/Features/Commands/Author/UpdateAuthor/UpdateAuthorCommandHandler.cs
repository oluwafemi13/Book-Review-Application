using Application.Contract.Persistence.Interface;
using Application.Features.Commands.author.DeleteAuthor;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.author.UpdateAuthor
{
    internal class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<UpdateAuthorCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository,
                                        ILogger<UpdateAuthorCommandHandler> logger,
                                        IMapper mapper)
        {
            _authorRepository = authorRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var check = await _authorRepository.GetAuthorByEmail(request.AuthorEmail);
            if (check == null) {
                throw new ArgumentException("Author not found");

            }
            var author = new Author()
            {
                
                AuthorName = request.AuthorName,
                AuthorEmail = request.AuthorEmail,
                AuthorBio = request.AuthorBio,
                LastModifiedDate = DateTime.Now,

            };
            //var map = _mapper.Map<Author>(request);
            await _authorRepository.UpdateAsync(author);
            return Unit.Value;
        }
    }
}
