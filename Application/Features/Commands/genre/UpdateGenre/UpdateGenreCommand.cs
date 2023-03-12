using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.genre.UpdateGenre
{
    public class UpdateGenreCommand: IRequest<HttpResponseMessage>
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public string? LastModifiedBy { get; set; }
        //public DateTime? LastModifiedDate { get; set; }
    }
}
