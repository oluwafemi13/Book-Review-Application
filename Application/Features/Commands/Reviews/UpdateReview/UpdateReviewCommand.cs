using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Reviews.UpdateReview
{
    public class UpdateReviewCommand : IRequest<bool>
    {
        public int ReviewId { get; set; }
        public string ReviewTitle { get; set; }
        public string review { get; set; }

       
    }
}
