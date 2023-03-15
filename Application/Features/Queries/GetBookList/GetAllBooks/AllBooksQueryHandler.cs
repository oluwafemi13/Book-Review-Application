﻿using Application.Contract.Persistence.Interface;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

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

        public async Task<IPagedList<BookVM>> Handle(AllBooksQuery request, CancellationToken cancellationToken)
        {
            var result = await _bookRepo.GetAllPagedAsync(request);
            return _mapper.Map<IPagedList<BookVM>>(result);
        }
    }
}
