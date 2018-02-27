using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FansPen.Web.Helpers
{
    public static class HTMLHelpers
    {
        public static IHtmlContent ToHTML(this IHtmlHelper htmlHelper, string ConvertString)
            => new HtmlString(ConvertString);
    }
}
