﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User: IdentityUser
    {
        public virtual Guid UserId { get; set; }
        public string Username { get; set; }
    }
}
