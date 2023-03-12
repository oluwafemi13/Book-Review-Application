using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Genre: EntityBase
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }

        //relationship between book and genre
        //public ICollection<Book> Books { get; set; }
        public ICollection<BookGenre> BookGenre { get; set; }
    }
}
