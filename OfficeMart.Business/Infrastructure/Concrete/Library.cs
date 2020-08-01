using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
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

        public List<SelectListItem> Categories
        {
            get
            {
                using (TransactionConfig.AppDbContext)
                {
                    var categories =new List<Category>();
                    var selectListItems = new List<SelectListItem>();

                    if (!memoryCache.TryGetValue("Categories", out categories))
                    {

                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                       .SetAbsoluteExpiration(TimeSpan.FromSeconds(60 * 60 * 1000));

                        memoryCache
                            .Set
                            (
                                "Categories", 
                                TransactionConfig
                                .AppDbContext
                                .Categories
                                .Where(x=>x.IsActive)
                                .ToList()
                                ,cacheEntryOptions
                            );
                    }

                    categories = memoryCache.Get("Categories") as List<Category>;
                    foreach (var item in categories)
                    {
                        selectListItems.Add(new SelectListItem()
                        { Text = item.CategoryName, Value = item.Id.ToString() });
                    }

                    return selectListItems;
                }
            }
        }
    }
}
