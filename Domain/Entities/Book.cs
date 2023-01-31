using System;
using System.Collections.Generic;
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
        public DateOnly DatePublished { get; set; }
        public string ISBN { get; set; }
        public string Language { get; set; }
        public string CoverImage { get; set; }

        //one to many relationship between book and Author
        public Author author { get; set; }

        //relationship between book and format
        public Format format { get; set; }

        //one to many relationship between ratings and book
        public static ICollection<Ratings> Ratings { get; set; }

        //relationship between book and review
        public ICollection<Reviews> Reviews { get; set; }

        //public double RatingAverage { get; set; } 


        /* //containing reviews and ratings of the book
         public (string, string) Reviews { get; set; }*/




    }
}
