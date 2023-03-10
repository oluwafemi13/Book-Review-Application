using Application.Contract.Persistence.Interface;
using Application.Exceptions;
using Application.Features.Commands.book.CreateBook;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.award.UpdateAward
{
    public class CreateAwardCommandHandler : IRequestHandler<CreateAwardCommand, Response>
    {
        private readonly IAwardRepository _awardRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<CreateAwardCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateAwardCommandHandler(IAwardRepository awardRepository,
                                         IAuthorRepository authorRepository,
                                         ILogger<CreateAwardCommandHandler> logger,
                                        IMapper mapper)
        {
            _awardRepository = awardRepository;
            _authorRepository = authorRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response> Handle(CreateAwardCommand request, CancellationToken cancellationToken)
        {
            var awardExist = await _awardRepository.GetByName(request.AwardTitle, request.AuthorId);
            var authorExist = await _authorRepository.GetAuthorById(request.AuthorId);
            if(authorExist== null)
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Author Not Found",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            
            var findYearWon = awardExist.Where(x => x.YearWon.Year == request.YearWon.Year).FirstOrDefault();

            if(findYearWon == null)
            {
                var award = new Award();
                award.YearWon = request.YearWon;
                award.DateCreated   = DateTime.Now;
                award.AwardTitle = request.AwardTitle;
                award.AuthorId = request.AuthorId;

                await _awardRepository.AddAsync(award);
                return new Response
                {
                    Status = "Success",
                    Message = "Award Successfully Created",
                    StatusCode = StatusCodes.Status200OK
                };

            }

            return new Response
            {
                Status = "Error",
                Message = "Duplicate Award Found",
                StatusCode = StatusCodes.Status400BadRequest
            };


        }
    }
}
