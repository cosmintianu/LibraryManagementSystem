using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    public class BooksServices
    {
        private readonly BookContext _context;

        public BooksServices(BookContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> AddBook(Book newBook)
        {
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            return newBook;
        }

        public async Task<bool> UpdateBook(int id, Book updatedBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Quantity = updatedBook.Quantity;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> LendBook(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
                return "Book not found.";

            if (book.Quantity <= 0)
                return "No copies available to lend.";

            book.Quantity -= 1;
            await _context.SaveChangesAsync();
            return "Book successfully lent.";
        }

        public async Task<string> ReturnBook(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
                return "Book not found.";

            if (book.Quantity >= book.TotalQuantity)
                return "All copies have already been returned.";

            book.Quantity += 1;
            await _context.SaveChangesAsync();
            return "Book successfully returned.";
        }
        public async Task<List<Book>> GetLowStockBooksAsync(int threshold)
        {
            return await _context.Books
                .Where(b => b.Quantity < threshold)
                .ToListAsync();
        }
    }
}
