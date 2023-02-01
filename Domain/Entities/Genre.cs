using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Genre
    {
        public Guid GenreId { get; set; }
        public string GenreName { get; set; } = Enum.GetName(typeof(GenreList),GenreList.Memoir);

        //relationship between book and genre
        public ICollection<Book> Books { get; set; }

    }
}
