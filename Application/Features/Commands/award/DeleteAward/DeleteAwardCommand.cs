using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.award.DeleteAward
{
    public class DeleteAwardCommand: IRequest
    {
        public int AwardId { get; set; }
    }
}
