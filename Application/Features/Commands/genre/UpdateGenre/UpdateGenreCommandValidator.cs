using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.genre.UpdateGenre
{
    public class UpdateGenreCommandValidator: AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {

            RuleFor(x => x.GenreName).NotEmpty().NotNull();
            RuleFor(x => x.LastModifiedBy).NotEmpty().NotNull();
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
