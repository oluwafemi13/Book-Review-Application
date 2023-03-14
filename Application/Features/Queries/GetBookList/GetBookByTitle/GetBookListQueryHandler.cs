﻿using Application.Contract.Persistence.Interface;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookList.GetBookByTitle
{
    public class GetBookListQueryHandler : IRequestHandler<GetBookListQuery, List<BookVM>>
    {
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;

        public GetBookListQueryHandler(IBookRepository bookRepo, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        public async Task<List<BookVM>> Handle(GetBookListQuery request, CancellationToken cancellationToken)
        {
            var bookList = await _bookRepo.GetByName(request.Title);
            
            return _mapper.Map<List<BookVM>>(bookList);
        }
    }
}
