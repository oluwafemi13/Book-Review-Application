using Application.Contract.Persistence.Interface;
using Application.Exceptions;
using Application.Features.Commands.author.DeleteAuthor;
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

namespace Application.Features.Commands.author.UpdateAuthor
{
    internal class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Response>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<UpdateAuthorCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _usermanager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository,
                                        ILogger<UpdateAuthorCommandHandler> logger,
                                        IMapper mapper,
                                        UserManager<User> userManager,
                                        RoleManager<IdentityRole> roleManager)
        {
            _authorRepository = authorRepository;
            _logger = logger;
            _mapper = mapper;
            _usermanager = userManager;
            _roleManager = roleManager;
        }
        public async Task<Response> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorExists = await _authorRepository.GetAuthorByEmail(request.AuthorEmail);
            var findUserId = await _usermanager.FindByEmailAsync(request.AuthorEmail);
            var userId = findUserId.Id;
            var authorID = authorExists.AuthorId;

            if (authorExists == null) 
                throw new ArgumentException("Author not found");
            if(findUserId == null)
                throw new NotFoundException(request.AuthorEmail);

            var findAuthorById = await _authorRepository.GetByIdAsync(authorID);

            findAuthorById.AuthorName = request.AuthorFirstName + " " + request.AuthorLastName;
            findAuthorById.AuthorBio = request.AuthorBio;
            findAuthorById.LastModifiedBy= request.LastModifiedBy;
            findAuthorById.LastModifiedDate= request.LastModifiedDate;
            

            var find = await _usermanager.FindByIdAsync(userId);
           
            find.FirstName= request.AuthorFirstName;
            find.LastName= request.AuthorLastName;
            find.Email= request.AuthorEmail;
            var result = await _usermanager.UpdateAsync(find);
                    
            if (!result.Succeeded)
                return new Response
                {
                Status = "Error",
                Message = "Update Unsuccessful!",
                StatusCode = StatusCodes.Status500InternalServerError
                };
                    //_logger.Log(LogLevel.Error, "Update UnSuccessful");
            await _authorRepository.UpdateAsync(findAuthorById);

            return new Response
                                {
                                    Status = "Successful",
                                    Message = "Update Successful!",
                                    StatusCode = StatusCodes.Status200OK
                                };
        }
    }
}
