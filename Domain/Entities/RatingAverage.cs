using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RatingAverage
    {
        public int Id { get; set; }

        public decimal AverageRating { get; set; }


        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        public Book book { get; set; }
    }
}
