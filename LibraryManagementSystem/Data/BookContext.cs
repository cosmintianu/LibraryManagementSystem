using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }
        public DbSet<LibraryManagementSystem.Models.Book> Books { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    Quantity = 192,
                    TotalQuantity = 200
                },
                new Book
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    Quantity = 196,
                    TotalQuantity = 200
                },
                new Book
                {
                    Id = 3,
                    Title = "1984",
                    Author = "George Orwell",
                    Quantity = 194,
                    TotalQuantity = 200
                },
                new Book
                {
                    Id = 4,
                    Title = "Pride and Prejudice",
                    Author = "Jane Austen",
                    Quantity = 181,
                    TotalQuantity = 200
                },
                new Book
                {
                    Id = 5,
                    Title = "Moby-Dick",
                    Author = "Herman Melville",
                    Quantity = 185,
                    TotalQuantity = 200
                }
                );
        }
    }
}
