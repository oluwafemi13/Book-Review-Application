using Application.Contract.Persistence.Interface;
using Application.Features.Commands.book.CreateBook;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.book.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _BookRepository;
        //private readonly IFormatRepository _formatRepository;
        private readonly ILogger<DeleteBookCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteBookCommandHandler(IBookRepository BookRepository,
                                        ILogger<DeleteBookCommandHandler> logger,
                                        IMapper mapper)
        {
            
            _BookRepository = BookRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var search = await _BookRepository.GetAsync(x => x.BookId == request.BookId);
            if (search == null)
            {
                _logger.LogInformation($"Book {request.BookId}not found");

            }
            await _BookRepository.DeleteBookAndFormat(request.BookId);
            return Unit.Value;
        }
    }
}
