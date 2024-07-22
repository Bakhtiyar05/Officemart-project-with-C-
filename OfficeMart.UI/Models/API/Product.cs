using System;
using Newtonsoft.Json;

namespace OfficeMart.UI.Models.API
{
    public class Product
    {
        public string Code { get; set; }
        public string GUID { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string DescriptionImages { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }
        public bool Status { get; set; }
        [JsonProperty(PropertyName = "CategoryGUİD")]
        public string CategoryGUID { get; set; }
    }
}

