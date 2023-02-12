﻿using Application.Contract.Persistence.Interface;
using Application.Features.Commands.genre.CreateGenre;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.genre.UpdateGenre
{
    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ILogger<UpdateGenreCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateGenreCommandHandler(IGenreRepository genreRepository,
                                        ILogger<UpdateGenreCommandHandler> logger,
                                        IMapper mapper)
        {
            _genreRepository = genreRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var find = await _genreRepository.GetByIdAsync(request.GenreId);
            if (find == null)
                _logger.LogInformation("Genre Does not Exist");
            request.LastModifiedDate = DateTime.Now;
            var map = _mapper.Map<Genre>(request);
            await _genreRepository.AddAsync(map);
            return Unit.Value;
        }
    }
}