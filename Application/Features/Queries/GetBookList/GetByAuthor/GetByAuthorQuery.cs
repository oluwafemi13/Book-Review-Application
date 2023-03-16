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
       /* public int PageIndex { get; set; }
        public int PageSize { get; set; }*/
        public string Author { get; set; }
        public GetByAuthorQuery(/*int pageIndex, int pageSize,*/ string author)
        {
           /* PageIndex = pageIndex;
            PageSize = pageSize;*/
            Author = author;
        }

        
        

       


    }
}
