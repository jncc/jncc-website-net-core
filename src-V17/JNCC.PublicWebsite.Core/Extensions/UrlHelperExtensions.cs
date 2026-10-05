using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Text;

namespace JNCC.PublicWebsite.Core.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string PaginationQueryString(this IUrlHelper urlHelper, long pageNumber, NameValueCollection filters, string pageNumberQueryStringName = "pageNumber")
        {
            var builder = new StringBuilder();

            builder.AppendFormat("?{0}={1}", pageNumberQueryStringName, pageNumber);

            foreach (var key in filters.AllKeys)
            {
                var uniqueValidValues = filters.GetValues(key)
                                               .Where(x => string.IsNullOrWhiteSpace(x) == false)
                                               .Distinct(StringComparer.OrdinalIgnoreCase);

                foreach (var value in uniqueValidValues)
                {
                    builder.AppendFormat("&{0}={1}", key, value);
                }
            }

            return builder.ToString();
        }
    }
}
