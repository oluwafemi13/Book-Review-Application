using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Application.Features.Queries.GetBookList.GetAll
{
    public class AllBooksQuery:RequestParameters, IRequest<IEnumerable<BookVM>>
    {
        public AllBooksQuery(int pageIndex, int pageSize)
        {
            PageSize= pageSize;
            PageIndex= pageIndex;
        }
      
        
    }
}
