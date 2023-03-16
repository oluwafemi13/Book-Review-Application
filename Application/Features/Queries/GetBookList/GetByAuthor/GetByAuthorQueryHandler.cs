using Application.Contract.Persistence.Interface;
using Application.DTO;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookList.GetByAuthor
{
    public class GetByAuthorQueryHandler : IRequestHandler<GetByAuthorQuery, IEnumerable<BookVM>>
    {
        private readonly IAuthorRepository _authorRepo;
        private readonly IBookRepository _bookrRepo;

        public GetByAuthorQueryHandler(IAuthorRepository authorRepo, IBookRepository bookrRepo)
        {
            _authorRepo = authorRepo;
            _bookrRepo = bookrRepo;
        }

        public async Task<IEnumerable<BookVM>> Handle(GetByAuthorQuery request, CancellationToken cancellationToken)
        {
            var list = new List<BookVM>();
            var book = new BookVM();
            var result = await _authorRepo.GetAsync(x=> x.AuthorName == request.Author);
            foreach(var item in result)
            {
                var books = await _bookrRepo.GetBook(item.AuthorId);
                
                
            }
            
            
        }
    }
}
