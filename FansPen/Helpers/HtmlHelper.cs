using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Web.Helpers
{
    public static class HTMLHelpers
    {
        public static IHtmlContent ToHTML(this IHtmlHelper htmlHelper, string ConvertString)
            => new HtmlString(ConvertString);

    }
}
