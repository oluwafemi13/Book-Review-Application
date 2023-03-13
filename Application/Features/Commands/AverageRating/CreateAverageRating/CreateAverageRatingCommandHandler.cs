using Application.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.AverageRating.CreateAverageRating
{
    internal class CreateAverageRatingCommandHandler : IRequestHandler<CreateAverageRatingCommand, Response>
    {
        public Task<Response> Handle(CreateAverageRatingCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
