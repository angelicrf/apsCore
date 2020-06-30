using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlsAndRoutes.Infrastructure.TagHelpers;
using Xunit;


namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void TestTagHelper()
        {
            var context = new TagHelperContext(new TagHelperAttributeList(),new Dictionary<object, object>(),"myuniqueid");

            var output = new TagHelperOutput("button",new TagHelperAttributeList(), (cache, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
           
            var tagHelper = new ButtonTagHelper
            {
                BsButtonColor = "testValue"
            };
            tagHelper.Process(context, output);
            
            Assert.Equal($"btn btn-{tagHelper.BsButtonColor}",output.Attributes["class"].Value);
        }
    }
}