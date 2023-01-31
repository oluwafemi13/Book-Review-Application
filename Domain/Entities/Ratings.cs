using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ratings
    {
        public Guid RatingId { get; set; }
        public double Rating { get; set; }
        //relationship between ratings and books
        public Book book { get; set; }

    }
}
