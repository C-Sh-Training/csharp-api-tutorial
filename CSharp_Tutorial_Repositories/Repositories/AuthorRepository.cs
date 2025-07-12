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
    public class AuthorRepository : IAuthorRepository
    {
        private BookManagementDbContext _context;

        public AuthorRepository(BookManagementDbContext context) 
        {
            _context = context;
        }
        public async Task<Author> AddAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var authorToDelete = await _context.Authors.FindAsync(id);
            if (authorToDelete == null)
            {
                return false; // Author not found
            }
            _context.Authors.Remove(authorToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Author>> GetAllAuthorAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int? id)
        {
            return await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Author> UpdateAuthorAsync(Author author)
        {
            _context.Authors.Update(author);
            return await _context.SaveChangesAsync().ContinueWith(t => author);
            // địt mẹ mày
        }
    }
}
