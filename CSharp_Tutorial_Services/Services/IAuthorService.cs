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
        Task<AuthorModel> AddAuthorAsync(AuthorModel author);
        Task<AuthorModel> UpdateAuthorAsync(AuthorModel author);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
