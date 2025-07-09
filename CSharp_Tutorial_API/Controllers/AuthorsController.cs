using CSharp_Tutorial_Services.BusinessObjects;
using CSharp_Tutorial_Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSharp_Tutorial_API.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var authors = await _authorService.GetAllAuthorsAsync();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            try
            {
                var author = await _authorService.GetAuthorByIdAsync(id);
                if (author == null)
                {
                    return NotFound(new { Message = "Author not found." });
                }
                return Ok(author);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] CreateAuthorModel author)
        {
            try
            {
                if (author == null)
                {
                    return BadRequest(new { Message = "Author cannot be null." });
                }

                // validate author properties use ModelState
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(new
                    {
                        Message = "Validation failed.",
                        Errors = errors
                    });
                }

                var addedAuthor = await _authorService.AddAuthorAsync(author);
                return CreatedAtAction(nameof(GetAllAuthors), new { id = addedAuthor.Id }, addedAuthor);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorModel author)
        {
            try
            {
                if (author == null)
                {
                    return BadRequest(new { Message = "Author cannot be null." });
                }
                // validate author properties use ModelState
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(new
                    {
                        Message = "Validation failed.",
                        Errors = errors
                    });
                }
                var updatedAuthor = await _authorService.UpdateAuthorAsync(id, author);
                if (updatedAuthor == null)
                {
                    return NotFound(new { Message = "Author not found." });
                }
                return Ok(updatedAuthor);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var deletedAuthor = await _authorService.DeleteAuthorAsync(id);
                if (deletedAuthor == null)
                {
                    return NotFound(new { Message = "Author not found." });
                }
                return Ok(new {Message = $"Deleted author '{deletedAuthor.Name}' successfully"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
