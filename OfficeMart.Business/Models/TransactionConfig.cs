using AutoMapper;
using OfficeMart.Business.Mappings;
using OfficeMart.Domain.Models.AppDbContext;
using System;

namespace OfficeMart.Business.Models
{
    public static class TransactionConfig
    {
        private static MappingProfile MappingProfile { get; set; }
        private static MapperConfiguration MapperConfiguration { get; set; }
        public static Mapper Mapper
        {
            get
            {
                return GetMapperInstance();
            }
        }

        public static OfficeMartContext AppDbContext
        {
            get
            {
                return new OfficeMartContext();
            }
        }

        private static Mapper GetMapperInstance()
        {
            MappingProfile = new MappingProfile();
            MapperConfiguration = new MapperConfiguration((x) => x.AddProfile(MappingProfile));
            var mapper = new Mapper(MapperConfiguration);

            return mapper;
        }
    }
}
