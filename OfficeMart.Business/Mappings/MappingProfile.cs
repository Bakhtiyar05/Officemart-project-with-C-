using AutoMapper;
using OfficeMart.Business.Dtos;
using OfficeMart.Domain.Models.Entities;
using System.Linq;

namespace OfficeMart.Business.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductDto>()
                .ForMember(x=>x.ProductImages,y=>y.MapFrom(e=>e.ProductImages.Select(x=>x.ImageName).ToList()))
                .ReverseMap();
            CreateMap<Color, ColorDto>().ReverseMap();
            CreateMap<ProductSize, ProductSizeDto>().ReverseMap();
            CreateMap<OrderNumber, OrderNumberDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
