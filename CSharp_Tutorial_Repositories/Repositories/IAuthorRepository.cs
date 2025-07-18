﻿using CSharp_Tutorial_Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Tutorial_Repositories.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAuthorAsync();
        Task<Author?> GetAuthorByIdAsync(int? id);
        Task<Author> AddAuthorAsync(Author author);
        Task<Author> UpdateAuthorAsync(Author author);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
