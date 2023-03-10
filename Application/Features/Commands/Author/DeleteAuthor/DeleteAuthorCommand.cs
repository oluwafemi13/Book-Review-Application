using Application.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.author.DeleteAuthor
{
    public class DeleteAuthorCommand: IRequest<Response>
    {
        public int AuthorId { get; set; }
    }
}
