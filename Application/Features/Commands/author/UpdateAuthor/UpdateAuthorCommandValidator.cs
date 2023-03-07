using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.author.UpdateAuthor
{
    public class UpdateAuthorCommandValidator: AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            /*RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is Required").NotNull().
                MaximumLength(60).WithMessage("Username must not Exceed 60 Characters");*/

            RuleFor(x => x.AuthorEmail).NotEmpty().WithMessage("Email is Required")
                .NotNull().EmailAddress().WithMessage("Must ba a valid Email Address");

            RuleFor(x => x.AuthorFirstName).NotEmpty().WithMessage("Input required for first name").NotNull()
                .MaximumLength(50).WithMessage("Name too Long. Maximum character is 50");

            RuleFor(x => x.AuthorLastName).NotEmpty().WithMessage("Input required for Last name").NotNull()
               .MaximumLength(50).WithMessage("Name too Long. Maximum character is 50");

            RuleFor(x => x.AuthorBio).NotEmpty().WithMessage("Input required for Biography").NotNull()
               .MaximumLength(500).WithMessage("Bio too Long. Maximum character is 500");
        }
    }
}
