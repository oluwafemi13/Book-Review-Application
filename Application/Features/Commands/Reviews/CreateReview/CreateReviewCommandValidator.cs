using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Reviews.CreateReview
{
    public class CreateReviewCommandValidator: AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.review).NotEmpty().NotNull().MaximumLength(300);
            RuleFor(x => x.bookId).NotEmpty().NotNull();
            RuleFor(x => x.ReviewTitle).NotEmpty().NotNull().MaximumLength(30);
            RuleFor(x => x.userId).NotEmpty().NotNull();

        }
    }
}
