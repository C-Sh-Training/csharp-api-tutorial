using CSharp_Tutorial_Services.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Tutorial_Services.Services
{
    public interface IAuthorService
    {
        // Interface methods for managing authors
        Task<List<AuthorModel>> GetAllAuthorsAsync();
        Task<AuthorModel> GetAuthorByIdAsync(int id);
        Task<AuthorModel> AddAuthorAsync(CreateAuthorModel author);
        Task<AuthorModel> UpdateAuthorAsync(int id, UpdateAuthorModel author);
        Task<AuthorModel> DeleteAuthorAsync(int id);
    }
}
