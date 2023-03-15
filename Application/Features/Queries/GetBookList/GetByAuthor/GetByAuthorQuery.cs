using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookList.GetByAuthor
{
    public class GetByAuthorQuery:IRequest<IEnumerable<BookVM>>
    {
        public string Author { get; set; }
        public GetByAuthorQuery(string author)
        {
            Author = author;
        }

       


    }
}
