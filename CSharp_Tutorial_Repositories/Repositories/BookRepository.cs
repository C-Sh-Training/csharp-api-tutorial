using CSharp_Tutorial_Repositories.DbContext;
using CSharp_Tutorial_Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Tutorial_Repositories.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookManagementDbContext _bookManagementDbContext;

        public BookRepository(BookManagementDbContext bookManagementDbContext)
        {
            _bookManagementDbContext = bookManagementDbContext;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            await _bookManagementDbContext.Books.AddAsync(book);
            _bookManagementDbContext.SaveChanges();
            return book;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var matchedBook = await _bookManagementDbContext.Books.FindAsync(id);
            if (matchedBook == null)
            {
                throw new Exception("Book not found!");
            }

            _bookManagementDbContext.Books.Remove(matchedBook);
            await _bookManagementDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Book>> GetAllBookAsync()
        {
            return await _bookManagementDbContext.Books.OrderBy(b => b.Id).ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            var books = await _bookManagementDbContext.Books.ToListAsync();
            var matchedBook = books.FirstOrDefault(b => b.Id.Equals(id));
            return matchedBook;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            _bookManagementDbContext.Books.Update(book);
            await _bookManagementDbContext.SaveChangesAsync();
            return book;
        }
    }
}
