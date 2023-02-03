using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public DateTime DatePublished { get; set; }
        public string ISBN { get; set; }
        public string Language { get; set; }
        public string CoverImage { get; set; }

        //one to many relationship between book and Author
        public Author author { get; set; }

        //relationship between book and format
        public Format format { get; set; }

        //one to many relationship between ratings and book
        public static ICollection<Rating> Ratings { get; set; }

        //relationship between book and review
        public ICollection<Review> Reviews { get; set; }

        //relationship between genre and books
        public ICollection<Genre> Genres { get; set; }

        [NotMapped]
        protected double ratingaverage = Ratings.Average(r => r.rating);
        //public double RatingAverage { get { return RatingAverage; } set { RatingAverage = ratingaverage; } }
        





    }
}
