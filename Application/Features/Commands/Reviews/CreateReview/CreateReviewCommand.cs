using Application.Model;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Reviews.CreateReview
{
    public class CreateReviewCommand: IRequest<Response>
    {
        //public Guid ReviewId { get; set; }
        [MaxLength(30)]
        public string ReviewTitle { get; set; }
        [MaxLength(300)]
        public string review { get; set; }

        [Required]
        //relationship between book and review
        public Guid bookId { get; set; }

        [Required]
        //relationship between review and user
        public string userId { get; set; }
    }
}
