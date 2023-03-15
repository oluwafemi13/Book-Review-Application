using Application.Contract.Persistence.Interface;
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

        public Task<IEnumerable<BookVM>> Handle(GetByAuthorQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
