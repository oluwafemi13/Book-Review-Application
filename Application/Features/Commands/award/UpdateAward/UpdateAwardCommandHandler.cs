using Application.Contract.Persistence.Interface;
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
    public class UpdateAwardCommandHandler : IRequestHandler<UpdateAwardCommand>
    {
        private readonly IAwardRepository _awardRepository;
        private readonly ILogger<UpdateAwardCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateAwardCommandHandler(IAwardRepository awardRepository,
                                        ILogger<UpdateAwardCommandHandler> logger,
                                        IMapper mapper)
        {
            _awardRepository = awardRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateAwardCommand request, CancellationToken cancellationToken)
        {
            var search = await _awardRepository.GetByIdAsync(request.AwardId);
            if (search == null)
            {
                _logger.LogError("Award Does not Exist");
                return Unit.Value;
            }
            
            var getallAwardsByAuthor = await _awardRepository.GetAllByIdAsync(request.AuthorId);
            
            foreach(var award in getallAwardsByAuthor)
            {
                
                if(award.AwardTitle== request.AwardTitle && award.YearWon.Date == request.YearWon.Date) 
                {
                    _logger.LogInformation($"Another award won on the same date {request.YearWon.Date} exists");

                }
            }
            //award.AwardId = request.AwardId;
            search.AuthorId = request.AuthorId;
            search.AwardTitle = request.AwardTitle;
            search.LastModifiedBy = request.LastModifiedBy;
            search.LastModifiedDate = request.LastModifiedDate;
            search.YearWon= request.YearWon;

            //var map = _mapper.Map<Award>(request);
             await _awardRepository.UpdateAsync(search);
            return Unit.Value;
        }
    }
}
