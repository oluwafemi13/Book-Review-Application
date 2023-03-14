using Application.Contract.Persistence.Interface;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookList.GetAllBooks
{
    public class AllBooksQueryHandler : IRequestHandler<AllBooksQuery, IReadOnlyList<BookVM>>
    {
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;

        public AllBooksQueryHandler(IBookRepository bookRepo,
                                    IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<BookVM>> Handle(AllBooksQuery request, CancellationToken cancellationToken)
        {
            var result = await _bookRepo.GetAllAsync();
            return _mapper.Map<IReadOnlyList<BookVM>>(result);
        }
    }
}
