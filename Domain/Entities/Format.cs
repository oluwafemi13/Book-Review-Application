using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Format: EntityBase
    {
        
        public int FormatId { get; set; }
        public string FormatType { get; set; }
        public int NumberOfPages { get; set; }
        //relationship between book and format

        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        public Book book { get; set; }
    }
}
