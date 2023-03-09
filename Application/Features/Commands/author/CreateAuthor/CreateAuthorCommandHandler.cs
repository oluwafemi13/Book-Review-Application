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
using Application.Model;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Commands.author.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Response>
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

        public async Task<Response> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var runCheck = await _authorRepository.GetAuthorByEmail(request.AuthorEmail);
            
            if(runCheck != null)
            {
                _logger.LogInformation($"User Already Exists");
                
                
            }
            
            var author = new Author()
            {
                AuthorEmail = request.AuthorEmail,
                AuthorName = request.AuthorName,
                AuthorBio = request.AuthorBio,
                
            };
           
            await _authorRepository.AddAsync(author);

            return StatusCodes()
        }

        
    }
}
