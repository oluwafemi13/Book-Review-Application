using MediatR;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Reviews.CreateReviewCommand
{
    public class CreateReviewCommand: IRequest<string>
    {
        public Guid ReviewId { get; set; }
        public string review { get; set; }
    }
}
