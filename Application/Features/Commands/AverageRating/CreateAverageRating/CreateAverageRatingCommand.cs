using Application.Model;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.AverageRating.CreateAverageRating
{
    public class CreateAverageRatingCommand: IRequest<Response>
    {
       /* public int Id { get; set; }

        public decimal AverageRating { get; set; }*/

        public Guid BookId { get; set; }
       
    }
}
