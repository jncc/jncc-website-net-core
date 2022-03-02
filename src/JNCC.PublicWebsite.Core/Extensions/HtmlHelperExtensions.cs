using JNCC.PublicWebsite.Core.Constants;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Globalization;
namespace JNCC.PublicWebsite.Core.Extensions
{
    public static class HtmlHelperExtensions
    {
        //public static string Directionality(this HtmlHelper htmlHelper, CultureInfo cultureInfo)
        //{
        //    if (cultureInfo == null || cultureInfo.TextInfo == null)
        //    {
        //        return HtmlTextDirectionalities.Auto;
        //    }

        //    return cultureInfo.TextInfo.IsRightToLeft ? HtmlTextDirectionalities.RightToLeft : HtmlTextDirectionalities.LeftToRight;
        //}

        //public static IHtmlString If(this HtmlHelper htmlHelper, bool condition, object value)
        //{
        //    return htmlHelper.IfElse(condition, value, null);
        //}

        //public static IHtmlString IfElse(this HtmlHelper htmlHelper, bool condition, object valueIfTrue, object valueIfFalse)
        //{
        //    var value = condition ? valueIfTrue : valueIfFalse;

        //    if (value == null)
        //    {
        //        return null;
        //    }

        //    return new MvcHtmlString(value.ToString());
        //}

        //public static HtmlString FoundationValidationSummary(this HtmlHelper htmlHelper)
        //{
        //    return htmlHelper.ValidationSummary(string.Empty, new { @class = "callout alert validation-summary" });
        //}

        //public static IHtmlString RawSanitized(this HtmlHelper htmlHelper, object value)
        //{
        //    if (value == null)
        //    {
        //        return MvcHtmlString.Empty;
        //    }

        //    return htmlHelper.RawSanitized(value.ToString());
        //}

        //public static IHtmlString RawSanitized(this HtmlHelper htmlHelper, string value)
        //{
        //    if (string.IsNullOrWhiteSpace(value))
        //    {
        //        return MvcHtmlString.Empty;
        //    }

        //    var sanitizer = new HtmlSanitizer();
        //    var valueSanitized = sanitizer.Sanitize(value);

        //    return MvcHtmlString.Create(value);
        //}
    }
}
