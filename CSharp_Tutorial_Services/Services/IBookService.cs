using CSharp_Tutorial_Repositories.Entities;
using CSharp_Tutorial_Services.BusinessObjects.BookModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Tutorial_Services.Services
{
    public interface IBookService
    {
        Task<List<GetAllBookModel>> GetAllBookAsync();
        Task<GetAllBookModel> GetBookByIdAsync(int id);
        Task<BookModel> AddBookAsync(AddBookModel book);
        Task<BookModel> UpdateBookAsync(int id, UpdateBookModel book) ;
        Task<bool> DeleteBookAsync(int id);
    }
}
