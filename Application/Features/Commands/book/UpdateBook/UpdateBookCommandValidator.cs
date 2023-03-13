using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.book.UpdateBook
{
    public class UpdateBookCommandValidator: AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.DatePublished).NotEmpty().NotNull();
            RuleFor(x => x.ISBN).NotEmpty().NotNull();
            RuleFor(x => x.BookTitle).NotEmpty().NotNull().MaximumLength(80).WithMessage("Book Title too Long");
            RuleFor(x => x.Description).NotNull().NotEmpty();
            //RuleFor(x => x.AuthorId).NotEmpty().NotNull();
            RuleFor(x => x.Summary).NotEmpty().NotNull();
            RuleFor(x => x.NumberOfPages).NotEmpty().NotNull();
            RuleFor(x => x.FormatType).NotEmpty().NotNull();
            RuleFor(x => x.Language).NotEmpty().NotNull();
        }
    }
}
