using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.Business.TagHelpers
{
    public class Pagination : TagHelper
    {
        public decimal TotalItems { get; set; }
        public decimal ItemsPerPage { get; set; }
        public decimal CurrentPage { get; set; }
        public string AspAction { get; set; }
        public string AspController { get; set; }
        public string Area { get; set; }
        public int CategoryId { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var builder = new StringBuilder();

            var totalPages = Math.Ceiling(TotalItems / ItemsPerPage);

            builder.Append("<ul class='page-numbers'>");

            for (int i = 1; i <= totalPages; i++)
            {
                if(Area == "")
                {
                    if (i == CurrentPage)
                        builder.Append($"<li><span class='page-numbers current'><a href='/{AspController}/{AspAction}/{CategoryId}/{i}'>{i}</a></span></li>");
                    else
                        builder.Append($"<li><a class='page-numbers' href='/{AspController}/{AspAction}/{CategoryId}/{i}'>{i}</a></li>");
                }
                else
                {
                    if (i == CurrentPage)
                        builder.Append($"<li><span class='page-numbers current'><a href='/{Area}/{AspController}/{AspAction}/{CategoryId}/{i}'>{i}</a></span></li>");
                    else
                        builder.Append($"<li><a class='page-numbers' href='/{Area}/{AspController}/{AspAction}/{CategoryId}/{i}'>{i}</a></li>");
                }
            }

            if(Area == "")
            {
                if ((CurrentPage + 1) > totalPages)
                    builder.Append($"<li><a class='next page-numbers'><i class='fa fa-angle-right'></i></a></li>");
                else
                    builder.Append($"<li><a class='next page-numbers' href='/{AspController}/{AspAction}/{CategoryId}/{CurrentPage + 1}'><i class='fa fa-angle-right'></i></a></li>");
            }
            else
            {
                if ((CurrentPage + 1) > totalPages)
                    builder.Append($"<li><a class='next page-numbers'><i class='fa fa-angle-right'></i></a></li>");
                else
                    builder.Append($"<li><a class='next page-numbers' href='/{Area}/{AspController}/{AspAction}/{CategoryId}/{CurrentPage + 1}'><i class='fa fa-angle-right'></i></a></li>");
            }

            builder.Append("<ul/>");

            output.Content.SetHtmlContent(builder.ToString());

            return base.ProcessAsync(context, output);
        }
    }
}
