using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.rating.CreateRating
{
    public class CreateRatingCommand: Rating, IRequest<int>
    {
    }
}
