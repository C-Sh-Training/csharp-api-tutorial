using CSharp_Tutorial_Services.BusinessObjects.BookModels;
using CSharp_Tutorial_Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSharp_Tutorial_API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBook()
        {
            try
            {
                var books = await _bookService.GetAllBookAsync();

                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveBook(int id)
        {
            try
            {
                var bookToDelete = await _bookService.DeleteBookAsync(id);

                return Ok("You have remove book with ID = " + id + " successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewBook([FromBody] AddBookModel book)
        {
            try
            {
                var addedBook = await _bookService.AddBookAsync(book);
                return Ok(addedBook);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookModel book)
        {
            try
            {
                var updatedBook = await _bookService.UpdateBookAsync(id, book);
                return Ok(updatedBook);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
