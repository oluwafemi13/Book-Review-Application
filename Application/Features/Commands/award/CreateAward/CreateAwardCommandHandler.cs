using Application.Contract.Persistence.Interface;
using Application.Exceptions;
using Application.Features.Commands.book.CreateBook;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.award.UpdateAward
{
    public class CreateAwardCommandHandler : IRequestHandler<CreateAwardCommand, int>
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
        public async Task<int> Handle(CreateAwardCommand request, CancellationToken cancellationToken)
        {
            var awardExist = await _awardRepository.GetByName(request.AwardTitle, request.AuthorId);
            var award = new Award();
            if (awardExist != null)
            {
                foreach(var a in awardExist)
                {
                    if(a.YearWon.Date == request.YearWon.Date)
                    {
                        _logger.LogInformation($"Award won in {request.YearWon.Date} already exists");
                    }
                    
                    
                }
                award.AwardId = request.AwardId;
                award.AuthorId = request.AuthorId;
                award.AwardTitle = request.AwardTitle;
                award.YearWon = request.YearWon;
                var add1 = await _awardRepository.AddAsync(award);
                return add1.AwardId;
            }
            
            award.AwardId = request.AwardId;
            award.AuthorId = request.AuthorId;
            award.AwardTitle= request.AwardTitle;
            award.YearWon = request.YearWon;

            var add = await _awardRepository.AddAsync(award);
            return add.AwardId;
            

        }
    }
}
