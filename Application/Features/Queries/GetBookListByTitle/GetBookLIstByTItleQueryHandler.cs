using Application.Contract.Persistence.Interface;
using Application.Features.Queries.GetBookListByAuthor;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookListByTitle
{
    public class GetBookListByTitleQueryHandler : IRequestHandler<GetBookListByTitleQuery, List<BookVM>>
    {
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;

        public GetBookListByTitleQueryHandler(IBookRepository bookRepo, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }
        public async Task<List<BookVM>> Handle(GetBookListByTitleQuery request, CancellationToken cancellationToken)
        {
            var bookList = await _bookRepo.GetByNameAsync(request.BookTitle);
            return _mapper.Map<List<BookVM>>(bookList);
        }
    }
}
