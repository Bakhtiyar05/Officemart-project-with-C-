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
        public List<SelectListItem> Colors
        {
            get
            {
                using (TransactionConfig.AppDbContext)
                {
                    var colors = new List<Color>();
                    var selectListItems = new List<SelectListItem>();

                    if (!memoryCache.TryGetValue("Colors", out colors))
                    {

                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                       .SetAbsoluteExpiration(TimeSpan.FromSeconds(60 * 60 * 1000));

                        memoryCache
                            .Set
                            (
                                "Colors",
                                TransactionConfig
                                .AppDbContext
                                .Colors
                                .ToList()
                                , cacheEntryOptions
                            );
                    }

                    colors = memoryCache.Get("Colors") as List<Color>;
                    foreach (var item in colors)
                    {
                        selectListItems.Add(new SelectListItem()
                        { Text = item.ColorName, Value = item.Id.ToString() });
                    }

                    return selectListItems;
                }
            }
        }
        public List<SelectListItem> ProductSizes
        {
            get
            {
                using (TransactionConfig.AppDbContext)
                {
                    var productSizes = new List<ProductSize>();
                    var selectListItems = new List<SelectListItem>();

                    if (!memoryCache.TryGetValue("ProductSizes", out productSizes))
                    {

                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                       .SetAbsoluteExpiration(TimeSpan.FromSeconds(/*60 * 60 * 1000*/ 1));

                        memoryCache
                            .Set
                            (
                                "ProductSizes",
                                TransactionConfig
                                .AppDbContext
                                .ProductSizes
                                .ToList()
                                , cacheEntryOptions
                            );
                    }

                    productSizes = memoryCache.Get("ProductSizes") as List<ProductSize>;
                    foreach (var item in productSizes)
                    {
                        selectListItems.Add(new SelectListItem()
                        { Text = item.Size, Value = item.Id.ToString() });
                    }

                    return selectListItems;
                }
            }
        }
    }
}
