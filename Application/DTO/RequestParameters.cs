using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class RequestParameters
    {
        private int _pageSize = 20;
        const int _maxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
               _pageSize =  (value > _maxPageSize) ? _maxPageSize : value;
            }

        }
    }
}
