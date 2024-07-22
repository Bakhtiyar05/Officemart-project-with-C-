using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OfficeMart.Business.Infrastructure.Concrete
{
    public sealed class Library
    {
        public static readonly Lazy<Library> instance = new Lazy<Library>(() => new Library());
        public IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

        public static Library GetInstance()
        {
            return instance.Value;
        }
    }
}
