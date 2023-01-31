using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Format
    {
        public Guid FormatId { get; set; }
        public string FormatType { get; set; }
        public int NumberOfPages { get; set; }
        //relationship between book and format
        public Book book { get; set; }
    }
}
