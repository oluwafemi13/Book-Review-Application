using Application.Contract.Persistence.Interface;
using Application.Features.Queries.GetBookList.GetBookListByAuthor;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookList.GetBookListByAverageRating
{
    public class GetBookListByAverageRatingQueryHandler : IRequestHandler<GetBookListByAverageRatingQuery, List<BookVM>>
    {
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;

        public GetBookListByAverageRatingQueryHandler(IBookRepository bookRepo, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        public async Task<List<BookVM>> Handle(GetBookListByAverageRatingQuery request, CancellationToken cancellationToken)
        {
            var bookList = await _bookRepo.GetBookByRatingAverage(request.AverageRating);
            return _mapper.Map<List<BookVM>>(bookList);
        }
    }
}
