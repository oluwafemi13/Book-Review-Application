using Application.Contract.Persistence.Interface;
using Application.Features.Commands.award.DeleteAward;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.format.CreateFormat
{
    public class CreateFormatCommandHandler : IRequestHandler<CreateFormatCommand>
    {
        private readonly IFormatRepository _formatRepository;
        private readonly ILogger<CreateFormatCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateFormatCommandHandler(IFormatRepository formatRepository,
                                        ILogger<CreateFormatCommandHandler> logger,
                                        IMapper mapper)
        {
            _formatRepository= formatRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateFormatCommand request, CancellationToken cancellationToken)
        {

            var mapped = _mapper.Map<Format>(request);
            await _formatRepository.AddAsync(mapped);
            return Unit.Value;
        }
    }
}
