using Application.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.author.CreateAuthor
{
    public class CreateAuthorCommand:IRequest<Response>
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string? AuthorBio { get; set; }
    }
}
