using Application.Contract.Persistence.Interface;
using Application.Features.Commands.format.CreateFormat;
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

namespace Application.Features.Commands.genre.CreateGenre
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, Response>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ILogger<CreateGenreCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateGenreCommandHandler(IGenreRepository genreRepository,
                                        ILogger<CreateGenreCommandHandler> logger,
                                        IMapper mapper)
        {
            _genreRepository = genreRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var findGenre = await _genreRepository.GetByNameAsync(request.GenreName);
            
            if (findGenre != null)
            {
                _logger.LogInformation("Genre ALready Exists");
                return new Response
                {
                    Status = "Error",
                    Message = $"{request.GenreName} already exist",
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
                
            var map = _mapper.Map<Genre>(request);
            await _genreRepository.AddAsync(map);
            return new Response
            {
                Status = "success",
                Message = "Successfully created A Genre",
                StatusCode = 201
            };


        }
    }
}
