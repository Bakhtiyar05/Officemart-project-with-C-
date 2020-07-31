using AutoMapper;
using OfficeMart.Business.Dtos;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Business.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
