using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UrlsAndRoutes.Infrastructure.TagHelpers
{
    public class ButtonTagHelper : TagHelper
    {
        public string BsButtonColor { get; set; }
        public override void Process(TagHelperContext context,TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", $"btn btn-{BsButtonColor}");
        }
    }
}
