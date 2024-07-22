using System.Collections.Generic;

namespace OfficeMart.UI.Models.API
{
    public class Category
    {
        public string Code { get; set; }
        public string GUID { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public string Owner { get; set; }
        public List<SubCategory> SubCategory { get; set; } = new List<SubCategory>();
    }
}

