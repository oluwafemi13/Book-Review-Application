using Application.Contract.Persistence.Interface;
using Application.Features.Commands.format.CreateFormat;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.genre.CreateGenre
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, int>
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
        public async Task<int> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var find = await _genreRepository.GetByNameAsync(request.GenreName);
            if (find != null)
                _logger.LogInformation("Genre ALready Exists");
            var map = _mapper.Map<Genre>(request);
            await _genreRepository.AddAsync(map);
            return map.GenreId;


        }
    }
}
