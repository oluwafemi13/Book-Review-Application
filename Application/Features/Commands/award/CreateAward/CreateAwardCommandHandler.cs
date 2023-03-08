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
            
            var findYearWon = awardExist.Where(x => x.YearWon.Date == request.YearWon.Date).FirstOrDefault();

            if(findYearWon == null)
            {
                var award = new Award();
                award.YearWon = request.YearWon;
                award.DateCreated   = DateTime.Now;
                award.AwardTitle = request.AwardTitle;
                award.AuthorId = request.AuthorId;

                await _awardRepository.AddAsync(award);
                return request.AwardId;

            }

            return findYearWon.AwardId;

            
            
            
            
            

        }
    }
}
