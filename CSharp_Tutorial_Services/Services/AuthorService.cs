using AutoMapper;
using CSharp_Tutorial_Repositories.Entities;
using CSharp_Tutorial_Repositories.Repositories;
using CSharp_Tutorial_Services.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Tutorial_Services.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper) 
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }
        public async Task<AuthorModel> AddAuthorAsync(CreateAuthorModel authorModel)
        {
            // add author logic here
            try
            {
                // Validate the author model
                if (authorModel == null)
                {
                    throw new Exception("Author not be null");
                }

                // check if author already exists
                var existingAuthors = await _authorRepository.GetAllAuthorsAsync();
                var authorExists = existingAuthors.Any(a => a.Name.Equals(authorModel.Name, StringComparison.OrdinalIgnoreCase));
                if (authorExists)
                {
                    throw new Exception("Author already exists");
                }

                // author must have 18 years or older
                if (authorModel.DateOfBirth > DateTime.Now.AddYears(-18))
                {
                    throw new Exception("Author must be 18 years or older");
                }

                // Convert AuthorModel to Author entity
                var addAuthor = _mapper.Map<Author>(authorModel);

                //var authorEntity = new Author
                //{
                //    Name = authorModel.Name,
                //    Biography = authorModel.Biography,
                //    DateOfBirth = authorModel.DateOfBirth
                //};

                // Add the author to the repository
                var addedAuthor = await _authorRepository.AddAuthorAsync(addAuthor);

                // Convert the added Author entity back to AuthorModel
                //var addedAuthorModel = new AuthorModel
                //{
                //    Id = addedAuthor.Id,
                //    Name = addedAuthor.Name,
                //    Biography = addedAuthor.Biography,
                //    DateOfBirth = addedAuthor.DateOfBirth
                //};
                return _mapper.Map<AuthorModel>(addedAuthor);
            }
            catch
            {
                throw;
            }
        }

        public async Task<AuthorModel> DeleteAuthorAsync(int id)
        {
            // check if author exists
            var deleteAuthor = await _authorRepository.GetAuthorByIdAsync(id);
            if (deleteAuthor == null)
            {
                throw new Exception("Author not found");
            }

            // check if author has books
            if (deleteAuthor.Books != null && deleteAuthor.Books.Count() > 0)
            {
                throw new Exception("Cannot delete author with existing books");
            }

            var result = await _authorRepository.DeleteAuthorAsync(deleteAuthor);
            if (!result)
            {
                throw new Exception("Failed to delete author");
            }

            // Convert the deleted Author entity back to AuthorModel
            return _mapper.Map<AuthorModel>(deleteAuthor);
        }

        public async Task<List<AuthorModel>> GetAllAuthorsAsync()
        {
            try
            {
                var authors = await _authorRepository.GetAllAuthorsAsync();

                // convert the list of Author entities to AuthorModel (mapper)
                var authorModels = _mapper.Map<List<AuthorModel>>(authors);

                //var authorModels = authors.Select(a => new AuthorModel
                //{
                //    Id = a.Id,
                //    Name = a.Name,
                //    Biography = a.Biography,
                //    DateOfBirth = a.DateOfBirth
                //}).ToList();

                return authorModels;
            }
            catch
            {
                throw new Exception("An error occurred while retrieving authors");
            }
        }

        public async Task<AuthorModel> GetAuthorByIdAsync(int id)
        {
            try
            {
                var author = await _authorRepository.GetAuthorByIdAsync(id);
                return _mapper.Map<AuthorModel>(author);
            }
            catch
            {
                throw;
            }
        }

        public async Task<AuthorModel> UpdateAuthorAsync(int id, UpdateAuthorModel authorModel)
        {
            try
            {
                // Validate the author model
                // author must have 18 years or older
                if (authorModel.DateOfBirth > DateTime.Now.AddYears(-18))
                {
                    throw new Exception("Author must be 18 years or older");
                }

                // check existing author
                var existingAuthors = await _authorRepository.GetAuthorByIdAsync(id);
                if (existingAuthors == null)
                {
                    throw new Exception("Author not found");
                }

                // check name uniqueness
                var allAuthors = await _authorRepository.GetAllAuthorsAsync();
                var authorExists = allAuthors.Any(a => a.Name.Equals(authorModel.Name, StringComparison.OrdinalIgnoreCase) && a.Id != id);
                if (authorExists)
                {
                    throw new Exception("Author with the same name already exists");
                }

                // Convert UpdateAuthorModel to Author entity
                // dam bao la mot object giong nhau
                _mapper.Map(authorModel, existingAuthors);

                // save changes to the repository
                var updatedAuthor = await _authorRepository.UpdateAuthorAsync(existingAuthors);

                // Convert the updated Author entity back to AuthorModel
                return _mapper.Map<AuthorModel>(updatedAuthor);
            }
            catch
            {
                throw;
            }
        }
    }
}
