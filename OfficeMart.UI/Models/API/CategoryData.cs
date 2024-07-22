using System.Collections.Generic;
using Newtonsoft.Json;

namespace OfficeMart.UI.Models.API
{
    public class CategoryData
    {
        [JsonProperty(PropertyName = "Category")]
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}

