using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Author.CreateAuthor
{
    public class CreateAuthorCommand:IRequest<int>
    {
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorBio { get; set; }
    }
}
