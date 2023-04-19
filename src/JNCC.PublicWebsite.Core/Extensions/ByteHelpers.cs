using System;

namespace JNCC.PublicWebsite.Core.Extensions
{
    public static class ByteHelpers
    {
        public static string FormatBytesForDisplay(long byteCount)
        {
            if (byteCount > 0)
                return SizeSuffix(byteCount);
            else
                return String.Empty;
        }

        // https://stackoverflow.com/a/14488941/40759

        static readonly string[] SizeSuffixes =
                        { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        static string SizeSuffix(long value, int decimalPlaces = 1)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }

            int i = 0;
            decimal dValue = (decimal)value;
            while (Math.Round(dValue, decimalPlaces) >= 1000)
            {
                dValue /= 1024;
                i++;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", dValue, SizeSuffixes[i]);
        }
    }
}
