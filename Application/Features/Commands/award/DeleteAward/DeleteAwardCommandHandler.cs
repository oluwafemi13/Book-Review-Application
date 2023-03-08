using Application.Contract.Persistence.Interface;
using Application.Features.Commands.award.UpdateAward;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.award.DeleteAward
{
    public class DeleteAwardCommandHandler : IRequestHandler<DeleteAwardCommand>
    {
        private readonly IAwardRepository _awardRepository;
        private readonly ILogger<DeleteAwardCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteAwardCommandHandler(IAwardRepository awardRepository,
                                        ILogger<DeleteAwardCommandHandler> logger,
                                        IMapper mapper)
        {
            _awardRepository = awardRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteAwardCommand request, CancellationToken cancellationToken)
        {
            var find = await _awardRepository.GetByIdAsync(request.AwardId);
            if (find is null)
                _logger.LogInformation($"Award {request.AwardId}Does Not Exist in the Database");
            else
                await _awardRepository.DeleteAsync(find);
            return Unit.Value;
        }
    }
}
