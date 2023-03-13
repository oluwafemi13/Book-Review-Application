using Application.Model;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.rating.CreateRating
{
    public class CreateRatingCommand: IRequest<Response>
    {
        public int RatingId { get; set; }
        public double rating { get; set; }


        //relationship between ratings and books
        public Guid BookId { get; set; }

        //relationship between user and ratings
        public string userId { get; set; }
    }
}
