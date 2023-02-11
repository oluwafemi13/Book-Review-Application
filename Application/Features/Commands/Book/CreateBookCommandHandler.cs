using Application.Contract.Persistence.Interface;
using Application.Features.Commands.author.CreateAuthor;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.book
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, string>
    {
        private readonly IBookRepository _BookRepository;
        private readonly ILogger<CreateBookCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateBookCommandHandler(IBookRepository BookRepository,
                                        ILogger<CreateBookCommandHandler> logger,
                                        IMapper mapper)
        {
            _BookRepository = BookRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var check = await _BookRepository.GetByNameAsync(request.BookTitle);
            if (check != null)
            {
                if(check.ISBN == request.ISBN)
                {
                    _logger.LogError($"Book Already Exists");
                    return check.ISBN;
                }
            }
            var map = _mapper.Map<Book>(request);
            var create = await _BookRepository.AddAsync(map);
            return create.BookId.ToString();
        }

        private bool validateISBN(string ISBN)
        {

        }

        private string convert10to13(string ISBN)
        {
            string prefix = "978";
            List<string> list = new List<string>();
            string clearedIn = ISBN.ToUpper().Replace("-", "").Replace(" ", "").Trim();
            string removeLastcharacter = clearedIn.Remove(9);
            list.Add(prefix+ removeLastcharacter);

        }
    }
}
