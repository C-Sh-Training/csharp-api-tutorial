using CSharp_Tutorial_Repositories.Entities;
using CSharp_Tutorial_Repositories.Repositories;
using CSharp_Tutorial_Services.BusinessObjects.BookModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Tutorial_Services.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BookModel> AddBookAsync(AddBookModel book)
        {
            try
            {
                if(book == null)
                {
                    throw new Exception("Invalid input");
                }
                var existingBooks = await _bookRepository.GetAllBookAsync();

                var duplicateName = existingBooks.Any(b => b.Title.Equals(book.Title, StringComparison.OrdinalIgnoreCase));

                if (duplicateName)
                {
                    throw new Exception("Duplicated book!");
                }

                //tao 1 instance de hung data cua book do user nhap vao
                var addedBook = new Book
                {
                    Title = book.Title,
                    AuthorId = book.AuthorId,
                    Description = book.Description,
                    PublishedDate = book.PublishedDate,
                    ISBN = book.ISBN,
                    Publisher = book.Publisher
                };

                var addBook = await _bookRepository.AddBookAsync(addedBook);

                //Convert sang book model
                var bookModel = new BookModel
                {
                    Id = addBook.Id,
                    Title = addBook.Title,
                    AuthorId = addBook.AuthorId,
                    Description = addBook.Description,
                    PublishedDate = addBook.PublishedDate,
                    ISBN = addBook.ISBN,
                    Publisher = addBook.Publisher
                };

                return bookModel;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            try
            {
                if(id == null)
                {
                    throw new Exception("ID can not be null");
                }
                await _bookRepository.DeleteBookAsync(id);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<GetAllBookModel>> GetAllBookAsync()
        {
            try
            {
                var books = await _bookRepository.GetAllBookAsync();

                if (books == null)
                {
                    throw new Exception("Not found any book");
                }

                var allBookModel = books.Select(a => new GetAllBookModel
                {
                    Id = a.Id,
                    ISBN = a.ISBN,
                    Title = a.Title,
                    AuthorId = a.AuthorId,
                    Description = a.Description,
                    PublishedDate = a.PublishedDate,
                    Publisher = a.Publisher
                }).ToList();
                return allBookModel;
            }
            catch
            {
                throw;
            }
        }

        public async Task<GetAllBookModel> GetBookByIdAsync(int id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "ID cannot be null");
            }
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero");
            }
            var existingBokk = await _bookRepository.GetBookByIdAsync(id);
            if (existingBokk == null)
            {
                throw new Exception($"Book with ID {id} not found");
            }

            var bookModel = new GetAllBookModel
            {
                Id = existingBokk.Id,
                ISBN = existingBokk.ISBN,
                Title = existingBokk.Title,
                AuthorId = existingBokk.AuthorId,
                Description = existingBokk.Description,
                PublishedDate = existingBokk.PublishedDate,
                Publisher = existingBokk.Publisher
            };
            return bookModel;
            
        }

        public async Task<BookModel> UpdateBookAsync(int id,UpdateBookModel updatedBook)
        {
            try
            {
                if(id <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero");
                }
                if (updatedBook == null)
                {
                    throw new ArgumentNullException(nameof(updatedBook), "Book cannot be null");
                }
                if (updatedBook == null)
                {
                   throw new Exception("Invalid input");
                }

                var existingBooks = _bookRepository.GetAllBookAsync();
                var duplicateName = existingBooks.Result.Any(b => b.Title.Equals(updatedBook.Title, StringComparison.OrdinalIgnoreCase) && b.Id != id);
                
                if(duplicateName)
                {
                    throw new Exception("Duplicated book!");
                }

                var matchedBook = await _bookRepository.GetBookByIdAsync(id);

                if (matchedBook == null)
                {
                    throw new Exception($"Book with ID {id} not found");
                }

                //update book entity
                matchedBook.Title = updatedBook.Title;
                matchedBook.AuthorId = updatedBook.AuthorId;
                matchedBook.ISBN = updatedBook.ISBN;
                matchedBook.Description = updatedBook.Description;
                matchedBook.PublishedDate = updatedBook.PublishedDate;
                matchedBook.Publisher = updatedBook.Publisher;
                
                var updateBook = await _bookRepository.UpdateBookAsync(matchedBook);

                var bookModel = new BookModel
                {
                    Id = id,
                    Title = updatedBook.Title,
                    Description = updatedBook.Description,
                    AuthorId = updatedBook.AuthorId,
                    ISBN = updatedBook.ISBN,
                    PublishedDate = updatedBook.PublishedDate,
                    Publisher = updatedBook.Publisher
                };

                return bookModel;
            }
            catch
            {
                throw;
            }
        }
    }
}
