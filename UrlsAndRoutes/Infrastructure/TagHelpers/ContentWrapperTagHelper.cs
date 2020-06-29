using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UrlsAndRoutes.Infrastructure.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "title")]
    public class ContentWrapperTagHelper :TagHelper
    {
        public bool IncludeHeader { get; set; } = true;
        public bool IncludeFooter { get; set; } = true;
        public string Title { get; set; }
        public override void Process(TagHelperContext context,TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", "m-1 p-1");
            TagBuilder title = new TagBuilder("h3");

            title.InnerHtml.Append(Title);
            TagBuilder container = new TagBuilder("div");

            container.Attributes["class"] = "bg-success m-1 p-1";
            container.InnerHtml.AppendHtml(title);

            if (IncludeHeader)
            {
                output.PreElement.SetHtmlContent(container);
            }
            if (IncludeFooter)
            {
                output.PostElement.SetHtmlContent(container);
            }

        }
    }
}