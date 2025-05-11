using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //static private List<Book> books = new List<Book>
        //{
        //    new Book
        //    {
        //        Id = 1,
        //        Title = "The Great Gatsby",
        //        Author = "F. Scott Fitzgerald",
        //        Quantity = 192
        //    },
        //    new Book
        //    {
        //        Id = 2,
        //        Title = "To Kill a Mockingbird",
        //        Author = "Harper Lee",
        //        Quantity = 196
        //    },
        //    new Book
        //    {
        //        Id = 3,
        //        Title = "1984",
        //        Author = "George Orwell",
        //        Quantity = 194
        //    },
        //    new Book
        //    {
        //        Id = 4,
        //        Title = "Pride and Prejudice",
        //        Author = "Jane Austen",
        //        Quantity = 181
        //    },
        //    new Book
        //    {
        //        Id = 5,
        //        Title = "Moby-Dick",
        //        Author = "Herman Melville",
        //        Quantity = 185
        //    }
        //};

        private readonly BookContext _context;
        private readonly BooksServices _booksServices;

        public BooksController(BookContext context, BooksServices booksServices)
        {
            _context = context;
            _booksServices = booksServices;
        }
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var books = await _booksServices.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _booksServices.GetBookById(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book newBook)
        {
            if (newBook == null)
                return BadRequest();

            var addedBook = await _booksServices.AddBook(newBook);
            return CreatedAtAction(nameof(GetBookById), new { id = addedBook.Id }, addedBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
        {
            var success = await _booksServices.UpdateBook(id, updatedBook);
            if (!success)
                return NotFound(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var success = await _booksServices.DeleteBook(id);
            if (!success)
                return NotFound();
            return NoContent();
        }


        [HttpGet("search")]
        public async Task<ActionResult<List<Book>>> SearchBooks([FromQuery] string? title, [FromQuery] string? author)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(b => b.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author.Contains(author));
            }

            var result = await query.ToListAsync();

            if (result.Count == 0)
            {
                return NotFound("No books matched the search criteria.");
            }

            return Ok(result);
        }

        [HttpPost("{id}/lend")]
        public async Task<IActionResult> LendBook(int id)
        {
            var result = await _booksServices.LendBook(id);

            if (result.Contains("not found") || result.Contains("No copies"))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("{id}/return")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var result = await _booksServices.ReturnBook(id);

            if (result.Contains("not found"))
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<List<Book>>> GetLowStockBooks([FromQuery] int threshold = 3)
        {
            var lowStockBooks = await _booksServices.GetLowStockBooksAsync(threshold);
            return Ok(lowStockBooks);
        }

    }
}
