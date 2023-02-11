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

namespace Application.Features.Commands.book.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IBookRepository _BookRepository;
        private readonly ILogger<UpdateBookCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IBookRepository BookRepository,
                                        ILogger<UpdateBookCommandHandler> logger,
                                        IMapper mapper)
        {
            _BookRepository = BookRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var search = await _BookRepository.GetByGuidAsync(request.BookId);
            if(search == null)
            {
                _logger.LogError($"Book with Id {request.BookId} not found");
                throw new NotFoundException(nameof(request.BookTitle));
            }
            var map = _mapper.Map<Book>(request);
            await _BookRepository.UpdateAsync(map);
            return Unit.Value;
        }
    }
}
