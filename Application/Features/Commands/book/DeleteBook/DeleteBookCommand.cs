using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.book.DeleteBook
{
    public class DeleteBookCommand: IRequest
    {
        public Guid BookId { get; set; }
    }
}
