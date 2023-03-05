using Application.Contract.Persistence.Interface;
using Application.Exceptions;
using Application.Features.Commands.author.DeleteAuthor;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorExists = await _authorRepository.GetAuthorByEmail(request.AuthorEmail);
            if (authorExists == null) 
                throw new ArgumentException("Author not found");

            var findUser =await _usermanager.FindByEmailAsync(request.AuthorEmail);
            if(findUser == null)
                throw new NotFoundException(request.AuthorEmail+ "Was  Not found");

            var author = new Author()
            {
                AuthorId= authorExists.AuthorId,
                AuthorName = request.AuthorFirstName+ " "+ request.AuthorLastName,
                AuthorEmail = request.AuthorEmail,
                AuthorBio = request.AuthorBio,
                LastModifiedBy = request.LastModifiedBy,
                LastModifiedDate = DateTime.Now,

            };

            var user = new User()
            {
                Email = request.AuthorEmail,
                FirstName = request.AuthorFirstName,
                LastName = request.AuthorLastName,
                

            };
            var result = await _usermanager.UpdateAsync(user);
            if (!result.Succeeded)
                _logger.Log(LogLevel.Error, "Update UnSuccessful");
            await _authorRepository.UpdateAsync(author);
            return Unit.Value;
        }
    }
}
