using Application.Contract.Persistence.Interface;
using Application.Exceptions;
using Application.Features.Commands.author.CreateAuthor;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.author.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Response>
    {

        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<DeleteAuthorCommandHandler> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository,
                                        ILogger<DeleteAuthorCommandHandler> logger,
                                        UserManager<User> userManager,
                                        IMapper mapper)
        {
            _authorRepository = authorRepository;
            _userManager= userManager;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {

            
            var runCheck = await _authorRepository.GetAuthorById(request.AuthorId);
            
            if (runCheck == null)
            {
                _logger.LogError($"user with user id {request.AuthorId} was not found");
            }
            var checkUser = await _userManager.FindByEmailAsync(runCheck.AuthorEmail);
            if (checkUser == null)
                _logger.LogInformation($"Author with Email: {runCheck.AuthorEmail} could not be found in the user Table");
            var result1 = await _userManager.DeleteAsync(checkUser);
            var result2 = await _authorRepository.DeleteAsync(runCheck);

            if ((!result1.Succeeded) && result2 == false)
                return new Response
                {
                    Status="Error",
                    Message="Deletion not Completed or Unsuccessful Delet Attempt",
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            return new Response
            {
               Status="Deleted",
               Message = "Deleted Successfully",
               StatusCode = StatusCodes.Status202Accepted
            };

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
