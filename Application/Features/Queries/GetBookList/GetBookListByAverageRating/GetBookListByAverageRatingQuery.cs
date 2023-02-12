using Application.Features.Queries.GetBookList.GetBookListByAuthor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetBookList.GetBookListByAverageRating
{
    public class GetBookListByAverageRatingQuery : IRequest<List<BookVM>>
    {
        public double AverageRating { get; set; }
        public GetBookListByAverageRatingQuery(double averageRating)
        {
            AverageRating = averageRating;
        }
    }
}
