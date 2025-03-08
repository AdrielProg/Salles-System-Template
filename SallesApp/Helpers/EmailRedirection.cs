using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SallesApp.Helpers
{
    public class EmailRedirection : TagHelper
    {
        public string EmailAddress { get; set; }
        public string Content { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:" + EmailAddress);
            output.Content.SetContent(Content);
        }
    }
}
