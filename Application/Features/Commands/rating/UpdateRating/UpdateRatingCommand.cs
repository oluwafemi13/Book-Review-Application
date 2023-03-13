using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.rating.UpdateRating
{
    public class UpdateRatingCommand:IRequest<bool>
    {
        public int RatingId { get; set; }
        public decimal rating { get; set; }
    }
}
