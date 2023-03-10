using Application.Features.Commands.award.UpdateAward;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.award.CreateAward
{
    public class CreateAwardCommandValidator: AbstractValidator<CreateAwardCommand>
    {

        public CreateAwardCommandValidator()
        {
            RuleFor(x => x.AuthorId).NotEmpty().WithMessage("Input required for Authors Id");
            RuleFor(x => x.AwardTitle).NotEmpty()
                                      .WithMessage("Input required for Award Title")
                                      .NotNull().MaximumLength(30)
                                      .WithMessage("Title Exceed maximum length of 30");
            RuleFor(x => x.YearWon).NotEmpty().WithMessage("Input required for Year Won").NotNull();
        }
    }
}
