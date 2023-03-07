using Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User: IdentityUser
    {
        //public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ForeignKey("RoleId")]
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<Rating> ratings { get; set; }

        public ICollection<Review> reviews { get; set; }
    }
}
