using Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

       // public string Role { get; set; }

        public ICollection<Rating> ratings { get; set; }

        public ICollection<Review> reviews { get; set; }
    }
}
