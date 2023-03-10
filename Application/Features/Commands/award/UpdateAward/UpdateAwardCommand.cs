using Application.Model;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.award.UpdateAward
{
    public class UpdateAwardCommand: IRequest<Response>
    {
        public int AwardId { get; set; }
        public string AwardTitle { get; set; }

        public DateTime YearWon { get; set; }

        public int AuthorId { get; set; }
        public string? LastModifiedBy { get; set; }
       
        public DateTime? LastModifiedDate { get; set; } = DateTime.Now;
    }
}
