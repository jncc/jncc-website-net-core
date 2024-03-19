using HeyRed.MarkdownSharp;
using Microsoft.AspNetCore.Html;
using System;
using System.IO;

namespace JNCC.PublicWebsite.Core.Utilities
{
    public static class AssetUtility
    {
        public static string GetIconName(string resourceType)
        {
            switch (resourceType)
            {
                case "publication": return "fa-file-alt";
                case "series": return "fa-clock";
                default: return "fa-table";
            }
        }

        public static string GetFileNameForDisplay(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
                return Path.GetFileName(uri.LocalPath);
            else
                return String.Empty;
        }

        public static string EnsureHttpsLinksForDataDotJncc(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
            {
                if (uri.Host == "data.jncc.gov.uk")
                {
                    var b = new UriBuilder(uri)
                    {
                        Scheme = Uri.UriSchemeHttps,
                        Port = -1 // default port for scheme
                    };

                    return b.ToString();
                }
            }

            return url;
        }

        public static string GetFileExtensionForDisplay(string fileExtension)
        {
            if (!string.IsNullOrEmpty(fileExtension))
                return fileExtension.ToUpper();
            else
                return String.Empty;
        }

        public static HtmlString GetFormattedMarkdown(string input)
        {
            var parser = new Markdown();

            return new HtmlString(parser.Transform(input));
        }
    }
}
