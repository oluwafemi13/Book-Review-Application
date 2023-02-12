using Application.Features.Queries.GetBookList.GetBookListByAuthor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookList.GetBookListByTitle
{
    public class GetBookListByTitleQuery : IRequest<List<BookVM>>
    {
        public string BookTitle { get; set; }

        public GetBookListByTitleQuery(string bookTitle)
        {
            BookTitle = bookTitle;
        }
    }
}
