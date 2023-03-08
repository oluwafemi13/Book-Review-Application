using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Award: EntityBase
    {
        public int AwardId { get; set; }
        public string AwardTitle { get; set; }

        public DateTime YearWon { get; set; }

        //relationship between author and awards
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author author { get; set; }
    }
}
