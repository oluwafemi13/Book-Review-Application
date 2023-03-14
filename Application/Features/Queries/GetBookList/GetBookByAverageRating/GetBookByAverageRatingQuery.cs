using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookList.GetBookByAverageRating
{
    public class GetBookByAverageRatingQuery : IRequest<List<BookVM>>
    {
        [Required]
        public decimal AverageRating { get; set; }
        public GetBookByAverageRatingQuery(decimal averageRating)
        {
            AverageRating = averageRating;
        }
    }
}
