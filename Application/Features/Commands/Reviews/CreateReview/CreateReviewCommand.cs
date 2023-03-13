using Application.Model;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Reviews.CreateReviewCommand
{
    public class CreateReviewCommand: IRequest<Response>
    {
        public Guid ReviewId { get; set; }
        public string ReviewTitle { get; set; }
        public string review { get; set; }

        //relationship between book and review
        public Guid bookId { get; set; }

        //relationship between review and user
        public string userId { get; set; }
    }
}
