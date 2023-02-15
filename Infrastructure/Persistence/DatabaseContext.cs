using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class DatabaseContext: IdentityDbContext<User>
    {

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }*/
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
        public DbSet<RatingAverage> RatingAverages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RatingAverage>().Property(x => x.AverageRating).HasColumnType<decimal>("decimal");
            modelBuilder.Entity<Rating>().Property(x => x.rating).HasColumnType<decimal>("decimal");
            //modelBuilder.Entity<IdentityUserLogin>().HasKey(x => x.UserId);

        }

    }
}
