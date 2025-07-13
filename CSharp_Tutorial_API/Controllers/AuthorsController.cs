using CSharp_Tutorial_Services.BusinessObjects.AuthorModels;
using CSharp_Tutorial_Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<IActionResult> GetAllAuthor()
        {
            try
            {
                var authors = await _authorService.GetAllAuthorAsync();
                return Ok(authors);
            }
            catch
            {
                return BadRequest(new
                {
                    Message = "An error occurred while retrieving authors."
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] GetAllAuthorModel author)
        {
            try
            {
                //Check author is null
                if (author == null)
                {
                    return BadRequest(new { Message = "Author can not be null" });
                }
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(new
                    {
                        Message = "Validation failed.",
                        Errors = errors });
                }

                var addedAuthor = await _authorService.AddAuthorAsync(author);
                return Ok(addedAuthor);
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
                return Ok(author);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }  
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAuthorById([FromQuery] int? id, [FromBody] UpdateAuthorModel updateAuthorModel)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest(new { Message = "Id can not be null!" });
                }

                var authorToUpdate = await _authorService.UpdateAuthorAsync(id, updateAuthorModel);

                return Ok(updateAuthorModel);

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
                return Ok(deletedAuthor);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
