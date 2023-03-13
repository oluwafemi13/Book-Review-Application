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
        //public string AuthorId { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public DateTime DatePublished { get; set; }
        public string ISBN { get; set; }
        public string Language { get; set; }
        public string CoverImage { get; set; }

        //many to many relationship between book and Author
        //public ICollection<Author> author { get; set; }
        public ICollection<BookAuthor> BookAuthor { get; set; }

        //relationship between book and format
        public Format format { get; set; }

        //one to many relationship between ratings and book
        public static ICollection<Rating> Ratings { get; set; }

        //relationship between book and review
        public ICollection<Review> Reviews { get; set; }

        //relationship between genre and books
        //public ICollection<Genre> Genres { get; set; }
        public ICollection<BookGenre> BookGenre { get; set; }

        public RatingAverage AverageRating { get; set; }






    }
}
