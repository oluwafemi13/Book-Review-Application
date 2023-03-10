using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.award.UpdateAward
{
    public class UpdateAwardCommandValidator:AbstractValidator<UpdateAwardCommand>
    {
        public UpdateAwardCommandValidator()
        {
            RuleFor(x => x.AwardTitle).NotEmpty().WithMessage("AwardTitle Must have a value").NotNull();
            RuleFor(x=>x.AuthorId).NotEmpty().NotNull();
            RuleFor(x => x.AwardId).NotEmpty().NotNull();
            RuleFor(x => x.YearWon).NotEmpty().NotNull();
        }
    }
}
