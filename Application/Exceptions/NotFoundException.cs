using Domain.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class NotFoundException: ApplicationException
    {
        public NotFoundException(string name):
            base($"Entity {name} was not found")
        {

        }

        public NotFoundException(string name, object key) :
            base($"Entity {name} was not found!")
        {
                
        }
    }
}
