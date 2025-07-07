using CSharp_Tutorial_Repositories.Entities;
using CSharp_Tutorial_Repositories.Repositories;
using CSharp_Tutorial_Services.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Tutorial_Services.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository) 
        {
            _authorRepository = authorRepository;
        }
        public async Task<AuthorModel> AddAuthorAsync(AuthorModel author)
        {
            // add author logic here
            try
            {
                // Validate the author model
                if (author == null)
                {
                    throw new Exception("Author not be null");
                }

                // check if author already exists
                var existingAuthors = await _authorRepository.GetAllAuthorsAsync();
                var authorExists = existingAuthors.Any(a => a.Name.Equals(author.Name, StringComparison.OrdinalIgnoreCase));
                if (authorExists)
                {
                    throw new Exception("Author already exists");
                }

                // author must have 18 years or older
                if (author.DateOfBirth > DateTime.Now.AddYears(-18))
                {
                    throw new Exception("Author must be 18 years or older");
                }

                // Convert AuthorModel to Author entity
                var authorEntity = new Author
                {
                    Name = author.Name,
                    Biography = author.Biography,
                    DateOfBirth = author.DateOfBirth
                };

                // Add the author to the repository
                var addedAuthor = await _authorRepository.AddAuthorAsync(authorEntity);

                // Convert the added Author entity back to AuthorModel
                var addedAuthorModel = new AuthorModel
                {
                    Id = addedAuthor.Id,
                    Name = addedAuthor.Name,
                    Biography = addedAuthor.Biography,
                    DateOfBirth = addedAuthor.DateOfBirth
                };
                return addedAuthorModel;
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> DeleteAuthorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AuthorModel>> GetAllAuthorsAsync()
        {
            try
            {
                var authors = await _authorRepository.GetAllAuthorsAsync();

                // convert the list of Author entities to AuthorModel (mapper)
                var authorModels = authors.Select(a => new AuthorModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Biography = a.Biography,
                    DateOfBirth = a.DateOfBirth
                }).ToList();

                return authorModels;
            }
            catch
            {
                throw new Exception("An error occurred while retrieving authors");
            }
        }

        public Task<AuthorModel> GetAuthorByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AuthorModel> UpdateAuthorAsync(AuthorModel author)
        {
            throw new NotImplementedException();
        }
    }
}
