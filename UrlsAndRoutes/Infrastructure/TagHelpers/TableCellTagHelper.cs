using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UrlsAndRoutes.Infrastructure.TagHelpers
{

        [HtmlTargetElement("td", Attributes = "wrap")]
        public class TableCellTagHelper : TagHelper
        {
            public override void Process(TagHelperContext context,TagHelperOutput output)
            {
                output.PreContent.SetHtmlContent("<strong><i>");
                output.PostContent.SetHtmlContent("</i></strong>");
            }
        }
    
}
