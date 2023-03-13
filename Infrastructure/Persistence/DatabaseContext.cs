using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
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
        public DbSet<Role> Role { get; set; }

        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RatingAverage>().Property(x => x.AverageRating).HasColumnType<decimal>("decimal");
            modelBuilder.Entity<Rating>().Property(x => x.rating).HasColumnType<decimal>("decimal").HasPrecision(18, 1);
            

            modelBuilder.Entity<Book>().HasOne<Format>(f=> f.format)
                                       .WithOne(b=> b.book)
                                       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Author>().HasMany<Award>(aw=> aw.awards)
                                         .WithOne(au=> au.author)
                                         .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Award>().Property(x => x.YearWon).HasColumnType<DateTime>("date");
            modelBuilder.Entity<Book>().Property(x => x.BookId).HasColumnType<Guid>("uniqueidentifier");

            modelBuilder.Entity<BookAuthor>().HasKey(bc => new { bc.AuthorId, bc.BookId});
            modelBuilder.Entity<BookGenre>().HasKey(bg => new { bg.BookId, bg.GenreId });

            modelBuilder.Entity<BookAuthor>()
            .HasOne<Book>(sc => sc.Book)
            .WithMany(s => s.BookAuthor)
            .HasForeignKey(sc => sc.BookId);


            modelBuilder.Entity<BookGenre>()
                .HasOne<Genre>(sc => sc.Genre)
                .WithMany(s => s.BookGenre)
                .HasForeignKey(sc => sc.GenreId);
        }

    }
}
