using Application.Features.Queries.GetBookListByAuthor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookListByTitle
{
    public class GetBookListByTitleQuery: IRequest<List<BookVM>>
    {
        public string BookTitle { get; set; }

        public GetBookListByTitleQuery(string bookTitle)
        {
            BookTitle = bookTitle;
        }
    }
}
