using Application.Contract.Persistence.Interface;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookList.GetByAverageRating
{
    public class GetByAverageRatingQueryHandler : IRequestHandler<GetByAverageRatingQuery, IEnumerable<BookVM>>
    {
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;

        public GetByAverageRatingQueryHandler(IBookRepository bookRepo, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookVM>> Handle(GetByAverageRatingQuery request, CancellationToken cancellationToken)
        {
            var bookList = await _bookRepo.GetBookByRatingAverage(request.AverageRating);

            return bookList;
        }
    }
}
