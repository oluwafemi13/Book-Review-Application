using Application.Features.Queries.GetBookList.GetBookByTitle;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookList.GetBookByAverageRating
{
    public class GetBookByAverageRatingQuery : IRequest<List<BookVM>>
    {
        public decimal AverageRating { get; set; }
        public GetBookByAverageRatingQuery(decimal averageRating)
        {
            AverageRating = averageRating;
        }
    }
}
