using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookListByAuthor

{
    public class GetBookListQuery:IRequest<List<BookVM>>
    {
        public string AuthorName { get; set; }

        public GetBookListQuery(string authorName)
        {
            AuthorName = authorName ?? throw new ArgumentNullException(nameof(authorName));
        }
    }
}
