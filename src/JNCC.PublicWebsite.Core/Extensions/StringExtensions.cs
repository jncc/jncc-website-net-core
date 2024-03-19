using System;
using System.Collections.Generic;
using System.Linq;

namespace JNCC.PublicWebsite.Core.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> CleanSplit(this string value, char separator)
        {
            return value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
        }

        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        public static string[] SplitIso8601DateIfPossible(this string iso8601Date)
        {
            DateTime parsed;

            if (DateTime.TryParse(iso8601Date, out parsed))
            {
                return new[] { parsed.ToString("yyyy"), parsed.ToString("MM"), parsed.ToString("dd") };
            }
            else
            {
                return new[] { iso8601Date, "", "" };
            }
        }
    }
}
