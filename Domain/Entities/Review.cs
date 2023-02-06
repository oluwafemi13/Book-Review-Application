using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public string review { get; set; }

        //relationship between book and review
        public Book book { get; set; }

        //relationship between review and user
        public User user { get; set; }
    }
}
