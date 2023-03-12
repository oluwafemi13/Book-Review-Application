using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookAuthor
    {
        public int Id { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
