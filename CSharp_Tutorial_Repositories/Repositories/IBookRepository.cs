using CSharp_Tutorial_Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Tutorial_Repositories.Repositories
{
    public interface IBookRepository
    {
        // repository methods for managing books
        Task<List<Book>> GetAllBookAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book?> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);
    }
}
