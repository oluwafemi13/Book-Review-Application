using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Award
    {
        public Guid AwardId { get; set; }
        public string AwardTitle { get; set; }

        //relationship between author and awards
        public Author author { get; set; }
    }
}
