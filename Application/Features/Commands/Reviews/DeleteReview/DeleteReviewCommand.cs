using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Reviews.DeleteReview
{
    public class DeleteReviewCommand: IRequest
    {
        public Guid ReviewId { get; set; }
        public string ReviewTitle { get; set; }
    }
}
