using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class DatabaseContext: IdentityUserContext<User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {


        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Format> Formats { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rating> Ratings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().HasData(
               new Author
               {
                   AuthorId = new Guid("57a6d071-0e2b-4039-8b38-1c9491271291"),
                   AuthorName = "Ikechukwu",
                   AuthorBio = " A Renowned Author with 7 years of experience writing and Publishing some of the Newyork Best selling Books.",
                   AuthorEmail = "sundayjabikem@gmail.com",
                   awards =
                   {
                       new Award{
                           AwardTitle = "New York Best Seller 2020",
                           
                       },
                       new Award{
                           AwardTitle = "New York Best Seller 2021",

                       },
                   },
                   Books =
                   {
                       new Book{ISBN = "dad1cd13-ae2b-415e-9145-b429e6ca8ec2", BookId = new Guid("nd2d51c21 - 6b2c - 4055 - af72 - dd809420a26a"), BookTitle = "Harry the Stubborn Boy", DatePublished = new DateTime(), format ={FormatId = new Guid(),FormatType = "Hard Cover", NumberOfPages = 300, BookId = new Guid("nd2d51c21 - 6b2c - 4055 - af72 - dd809420a26a") }, CoverImage = "string", Description = "A book for clever readers" },
                       new Book{ISBN = "ncb7e07fc-e66a-4d0f-887a-9c272a64d92f" },
                       new Book{ISBN = "770be283-2cab-4550-9e87-8a1162421b30"}
                   }




               }

                );


        }
    }
}
