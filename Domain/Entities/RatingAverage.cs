using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RatingAverage
    {
        public int Id { get; set; }

        public double AverageRating { get; set; }



        public ICollection<Book> Books { get; set; }
    }
}
