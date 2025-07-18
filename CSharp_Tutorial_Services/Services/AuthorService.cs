﻿using CSharp_Tutorial_Repositories.Entities;
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

        public async Task<GetAllAuthorModel> AddAuthorAsync(GetAllAuthorModel author)
        {
            try
            {
                if(author == null)
                {
                    throw new Exception("Author can not be null!");
                }
                if (author.DateOfBirth > DateTime.Now.AddYears(-18)) 
                {
                    throw new Exception("Author must be older 18 years old!");
                }

                //Check existed author
                var existingAuthors = await _authorRepository.GetAllAuthorAsync();
                var authorExist = existingAuthors.Any(a => a.Name.Equals(author.Name, StringComparison.OrdinalIgnoreCase));
                if (authorExist) 
                {
                    throw new Exception("Author already existed!");
                }

                //Create new Author entity
                var entityAuthor = new Author {
                    Name = author.Name,
                    Biography = author.Biography,
                    DateOfBirth = author.DateOfBirth,
                };

                // Add the author to the repository
                var addedAuthor = await _authorRepository.AddAuthorAsync(entityAuthor);

                // Convert Author Entity to Author Model
                var addedAuthorModel = new GetAllAuthorModel
                {
                    Id = addedAuthor.Id,
                    Name = addedAuthor.Name,
                    Biography = addedAuthor.Biography,
                    DateOfBirth = addedAuthor.DateOfBirth,
                };

                return addedAuthorModel;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            try
            {
                var deleteAuthor = await _authorRepository.GetAuthorByIdAsync(id);
                
                if(deleteAuthor == null)
                {
                    throw new Exception("Author not found!");
                }

                if (deleteAuthor.Books != null && deleteAuthor.Books.Count > 0) 
                {
                    throw new Exception("Can not delete author because he/she wrote and publish book(s)");
                }

                var result = await _authorRepository.DeleteAuthorAsync(id);

                if(result == null)
                {
                    throw new Exception("Can not delete author!");
                }

                return true;

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<GetAllAuthorModel>> GetAllAuthorAsync()
        {
            try
            {
                var authors = await _authorRepository.GetAllAuthorAsync();

                //Convert the list of Author entities to AuthorModel
                var authorModels = authors.Select(a => new GetAllAuthorModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Biography = a.Biography,
                    DateOfBirth = a.DateOfBirth,
                }
                ).ToList();

                return authorModels;
            }
            catch
            {
                throw;
            }
        }

        public async Task<GetAllAuthorModel?> GetAuthorByIdAsync(int? id)
        {
            try
            {
                if(id == null)
                {
                    throw new Exception("Id can not be null!");
                }
                var exestingAuthor = await _authorRepository.GetAuthorByIdAsync(id);

                if(exestingAuthor == null)
                {
                    throw new Exception("Author is not existed!");
                }

                var authorModel = new GetAllAuthorModel
                {
                    Id = exestingAuthor.Id,
                    Name = exestingAuthor.Name,
                    Biography = exestingAuthor.Biography,
                    DateOfBirth = exestingAuthor.DateOfBirth,
                };

                return authorModel;
            }
            catch
            {
                throw ;
            }
        }

        public async Task<UpdateAuthorModel> UpdateAuthorAsync(int? id, UpdateAuthorModel updateAuthorModel)
        {
            try
            {
                if(updateAuthorModel == null)
                {
                    throw new Exception("Author can not be null!");
                }

                var authors = await _authorRepository.GetAllAuthorAsync();

                var matchedAuthor = await _authorRepository.GetAuthorByIdAsync(id);

                var authorToUpdate = new Author
                {
                    Name = updateAuthorModel.Name,
                    Biography = updateAuthorModel.Biography,
                    DateOfBirth = updateAuthorModel.DateOfBirth,
                };

                

                if(matchedAuthor == null)
                {
                    throw new Exception("Author not found!");
                }

                var reponseAuthor = new UpdateAuthorModel
                {
                    Name = matchedAuthor.Name,
                    Biography = matchedAuthor.Biography,
                    DateOfBirth = matchedAuthor.DateOfBirth
                };

                await _authorRepository.UpdateAuthorAsync(authorToUpdate);

                return reponseAuthor;
            }
            catch
            {
                throw;
            }
        }
    }
}
