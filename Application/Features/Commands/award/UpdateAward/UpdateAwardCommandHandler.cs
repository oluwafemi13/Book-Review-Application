using Application.Contract.Persistence.Interface;
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
    public class UpdateAwardCommandHandler : IRequestHandler<UpdateAwardCommand, Response>
    {
        private readonly IAwardRepository _awardRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<UpdateAwardCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateAwardCommandHandler(IAwardRepository awardRepository,
                                        ILogger<UpdateAwardCommandHandler> logger,
                                        IAuthorRepository authorRepository,
                                        IMapper mapper)
        {
            _awardRepository = awardRepository;
            _authorRepository= authorRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response> Handle(UpdateAwardCommand request, CancellationToken cancellationToken)
        {
            var search = await _awardRepository.GetByIdAsync(request.AwardId);
            if (search == null)
            {
                _logger.LogError("Award Does not Exist");
                return new Response
                                    {
                                        Status="Error",
                                        Message="Award Does not Exist",
                                        StatusCode = StatusCodes.Status404NotFound
                                    };
            }
            
            var getallAwardsByAuthor = await _awardRepository.GetAllByIdAsync(request.AuthorId);
            var getAuthorName = await _authorRepository.GetByIdAsync(request.AuthorId);
            
            foreach(var award in getallAwardsByAuthor)
            {
                
                if(award.AwardTitle== request.AwardTitle && award.YearWon.Date == request.YearWon.Date) 
                {
                    _logger.LogInformation($"Another award won on the same date {request.YearWon.Date} by the same Author {getAuthorName.AuthorName} exists");
                    return new Response
                    {
                        Status="Error",
                        Message=$"Another award won on the same date {request.YearWon.Date} Already exists",
                        StatusCode = StatusCodes.Status400BadRequest
                    };

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
            return new Response
            {
                Status = "Success",
                Message = "Award Successfully Updated",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
