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

        //one to many relationship between book and Author
        public Author author { get; set; }
        public string CpverImage { get; set; }
        //containing reviews and ratings of the book
        public (string, string) Reviews { get; set; }




    }
}
