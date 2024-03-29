﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.genre.DeleteGenre
{
    public class DeleteGenreCommand: IRequest<bool>
    {
        public int GenreId { get; set; }
    }
}
