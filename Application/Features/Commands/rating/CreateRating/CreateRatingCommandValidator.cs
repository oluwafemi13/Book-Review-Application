using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.rating.CreateRating
{
    public class CreateRatingCommandValidator:AbstractValidator<CreateRatingCommand>
    {
        public CreateRatingCommandValidator() 
        { 
            RuleFor(x => x.rating).NotEmpty().NotNull();
            RuleFor(x => x.userId).NotEmpty().NotNull();
            //RuleFor(x => x.RatingId).NotEmpty().NotNull();


        }
    }
}
