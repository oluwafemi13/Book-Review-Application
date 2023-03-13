using Application.Contract.Persistence.Interface;
using Application.Features.Commands.genre.CreateGenre;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.genre.DeleteGenre
{
    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, bool>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ILogger<DeleteGenreCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteGenreCommandHandler(IGenreRepository genreRepository,
                                        ILogger<DeleteGenreCommandHandler> logger,
                                        IMapper mapper)
        {
            _genreRepository = genreRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var check = await _genreRepository.GetByIdAsync(request.GenreId);
            if (check == null)
            {
                _logger.LogInformation("Genre Does not Exist");
                return false;
            }
                
            await _genreRepository.DeleteAsync(check);
            return true;
        }
    }
}
