using AutoMapper;
using CSharp_Tutorial_Repositories.Entities;
using CSharp_Tutorial_Services.BusinessObjects.AuthorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Tutorial_Services.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // CreateMap<Source, Destination>();
            // Example mapping for AuthorModel to GetAllAuthorModel
            CreateMap<Author, GetAllAuthorModel>();
            CreateMap<Author, AuthorModel>();
        }
    }
}
