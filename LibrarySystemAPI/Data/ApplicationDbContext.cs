using LibrarySystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystemAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Book> Books {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasData(
                    new Book
                    {
                        Id = 1,
                        Title = "Introduction to Data Structures",
                        Author = "Pascal Zury",
                        Genre = "Science",
                        PublicationDate = new DateTime(1945, 4, 10),
                        AvailabilityStatus = true,
                        Edition = "Tenth Edition",
                        Summary = "Linear Data Structures, Non Linear Data Structures"
                    },
                    
                    new Book
                    {
                        Id = 2,
                        Title = "Introduction to Electronics",
                        Author = "Eric Zury",
                        Genre = "Engineering",
                        PublicationDate = new DateTime(1955, 4, 8),
                        AvailabilityStatus = true,
                        Edition = "Eleventh Edition",
                        Summary = "Signals, Controls"
                    } ,

                    new Book
                    {
                        Id = 3,
                        Title = "Introduction to C#",
                        Author = "Pascal Zury",
                        Genre = "Science",
                        PublicationDate = new DateTime(1925, 4, 9),
                        AvailabilityStatus = true,
                        Edition = "First Edition",
                        Summary = "Objects and Classes in C#"
                    }
                );

            
        }

    }
}
