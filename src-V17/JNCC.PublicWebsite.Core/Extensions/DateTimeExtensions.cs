namespace JNCC.PublicWebsite.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static string LongDateWithFormat(this DateTime value)
        {
            string suffix;

            if (value.Day % 10 == 1)
            {
                suffix = "st";
            }
            else if (value.Day % 10 == 2)
            {
                suffix = "nd";
            }
            else if (value.Day % 10 == 3)
            {
                suffix = "rd";
            }
            else
            {
                suffix = "th";
            }

            return string.Format("{0}{1} {2} {3}", value.Day, suffix, value.ToString("MMM"), value.Year);
        }

        public static string LongDateWithTimeFormat(this DateTime value)
        {
            string suffix;

            if (value.Day % 10 == 1)
            {
                suffix = "st";
            }
            else if (value.Day % 10 == 2)
            {
                suffix = "nd";
            }
            else if (value.Day % 10 == 3)
            {
                suffix = "rd";
            }
            else
            {
                suffix = "th";
            }

            return string.Format("{4} on {0}{1} {2} {3}", value.Day, suffix, value.ToString("MMMM"), value.Year, value.ToString("HH:mm"));
        }

        public static string ToJobExpirationDate(this DateTime value)
        {
            return value.ToString("HH:mm on dddd, MMMM dd, yyyy");
        }

    }
}