﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.genre.CreateGenre
{
    public class CreateGenreCommandValidator:AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(x => x.GenreName).NotEmpty().NotNull();
            

        }
    }
}
