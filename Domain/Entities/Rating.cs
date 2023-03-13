using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Rating
    {
        public int RatingId { get; set; }
        public double rating { get; set; }


        //relationship between ratings and books
        public Book book { get; set; }

        //relationship between user and ratings
        public User user { get; set; }
    }
}
