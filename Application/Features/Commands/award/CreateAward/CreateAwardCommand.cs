using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.award.UpdateAward
{
    public class CreateAwardCommand : IRequest<int>
    {
        public int AwardId { get; set; }
        public string AwardTitle { get; set; }

        public DateTime YearWon { get; set; }

        public int AuthorId { get; set; }
    }
}
