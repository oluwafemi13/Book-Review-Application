using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Static
{
    public class UserRoles
    {
        public const string Admin = "Admin";  //access to all security levels
        public const string User = "User";   //leave comments and Reviews only
        public const string Author = "Author"; //add books and Read Reviews
    }
}
