using Application.Contract.Persistence.Interface;
using Application.Exceptions;
using Application.Features.Commands.book.CreateBook;
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

namespace Application.Features.Commands.book.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Response>
    {
        private readonly IBookRepository _BookRepository;
        private readonly IFormatRepository _formatRepository;
        private readonly ILogger<UpdateBookCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IBookRepository BookRepository,
                                        ILogger<UpdateBookCommandHandler> logger,
                                        IFormatRepository formatRepository,
                                        IMapper mapper)
        {
            _BookRepository = BookRepository;
            _formatRepository = formatRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var search = await _BookRepository.GetByGuidAsync(request.BookId);
                var findFormat = await _formatRepository.GetByGuidAsync(request.BookId);
                if (search == null)
                {
                    _logger.LogError($"Book with Id {request.BookId} not found");
                    return new Response
                    {
                        Status = "Error",
                        Message = "Book Does not Exist",
                        StatusCode=StatusCodes.Status404NotFound
                    };
                }

                var map = _mapper.Map<Book>(request);
                await _BookRepository.UpdateAsync(map);
                return Unit.Value;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        
    }
}
