﻿using CSharp_Tutorial_Repositories.Entities;
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
        Task<List<GetAllAuthorModel>> GetAllAuthorAsync();
        Task<GetAllAuthorModel?> GetAuthorByIdAsync(int? id);
        Task<GetAllAuthorModel> AddAuthorAsync(GetAllAuthorModel author);
        Task<UpdateAuthorModel> UpdateAuthorAsync(int? id, UpdateAuthorModel author);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
