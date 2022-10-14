using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiniMvc
{
    public static class HtmlExtensions
    {
        public static IHtmlContent RenderScript(
            this IHtmlHelper htmlHelper,
            string name
        )
        {
            var builder = new TagBuilder("script");
            builder.Attributes["type"] = "text/javascript";
            builder.Attributes["src"] = name;
            return builder;
        }
    }
}