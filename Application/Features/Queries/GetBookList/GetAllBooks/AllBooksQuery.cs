using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookList.GetAllBooks
{
    public class AllBooksQuery: IRequest<IReadOnlyList<BookVM>>
    {
        public AllBooksQuery()
        {

        }
    }
}
