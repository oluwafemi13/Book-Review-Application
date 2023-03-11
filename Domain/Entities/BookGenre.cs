using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookGenre
    {
        //public int Id { get; set; }

        public Guid BookId { get; set; }
        public Book book { get; set; }

        public int GenreId { get; set; }
        public Genre genre { get; set; }
    }
}
