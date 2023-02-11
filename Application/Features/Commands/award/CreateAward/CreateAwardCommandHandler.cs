using Application.Contract.Persistence.Interface;
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
        private readonly ILogger<CreateAwardCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateAwardCommandHandler(IAwardRepository awardRepository,
                                        ILogger<CreateAwardCommandHandler> logger,
                                        IMapper mapper)
        {
            _awardRepository = awardRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateAwardCommand request, CancellationToken cancellationToken)
        {
            var search = await _awardRepository.GetByNameAsync(request.AwardTitle);
            if (search != null && search.YearWon == request.YearWon)
                _logger.LogError("Award Already Exist");
            var map = _mapper.Map<Award>(request);
            var add = await _awardRepository.AddAsync(map);
            return add.AwardId;

        }
    }
}
