using Application.Features.Queries.GetBookList.GetBookByName;
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
        public double AverageRating { get; set; }
        public GetBookByAverageRatingQuery(double averageRating)
        {
            AverageRating = averageRating;
        }
    }
}
