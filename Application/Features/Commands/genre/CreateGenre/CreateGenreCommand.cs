using Application.Model;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.genre.CreateGenre
{
    public class CreateGenreCommand:EntityBase, IRequest<Response>
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
    }
}
