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

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorModel author)
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
    }
}
