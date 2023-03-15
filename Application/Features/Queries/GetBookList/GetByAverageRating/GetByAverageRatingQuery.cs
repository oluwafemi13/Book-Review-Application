using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookList.GetByAverageRating
{
    public class GetByAverageRatingQuery : IRequest<IEnumerable<BookVM>>
    {
        [Required]
        public decimal AverageRating { get; set; }
        public GetByAverageRatingQuery(decimal averageRating)
        {
            AverageRating = averageRating;
        }
    }
}
