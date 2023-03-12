using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Author:EntityBase
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorBio { get; set; }


        //relationship between book and author
        //public ICollection<Book> Books { get; set; }
        public ICollection<BookAuthor> BookAuthor { get; set; }
        //relationship between author and awards
        public ICollection<Award> awards { get; set; }
    }
}
