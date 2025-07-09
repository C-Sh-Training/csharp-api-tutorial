using AutoMapper;
using CSharp_Tutorial_Repositories.Entities;
using CSharp_Tutorial_Services.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Tutorial_Services.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // author mapper
            CreateMap<Author, AuthorModel>();
            CreateMap<CreateAuthorModel, Author>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id during mapping
                .ForMember(dest => dest.Books, opt => opt.Ignore()); // Ignore Books during mapping

            CreateMap<UpdateAuthorModel, Author>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id during mapping
                .ForMember(dest => dest.Books, opt => opt.Ignore()); // Ignore Books during mapping
        }
    }
}
