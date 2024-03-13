﻿using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static IOrderedEnumerable<T> OrderByFirstAvailableProperty<T>(this IEnumerable<T> enumerable, Func<T, IEnumerable<string>> properties)
        {
            return enumerable.OrderBy(x => properties.Invoke(x)
                                                     .Where(y => string.IsNullOrWhiteSpace(y) == false)
                                                     .FirstOrDefault()
                                     );
        }

        public static IOrderedEnumerable<T> ThenByFirstAvailableProperty<T>(this IOrderedEnumerable<T> enumerable, Func<T, IEnumerable<string>> properties)
        {
            return enumerable.ThenBy(x => properties.Invoke(x)
                                                    .Where(y => string.IsNullOrWhiteSpace(y) == false)
                                                    .FirstOrDefault()
                                    );
        }

        public static bool IsFirst<T>(this IEnumerable<T> enumerable, T element)
        {
            return enumerable.IndexOf(element) == 0;
        }

        public static IEnumerable<string> AllToString<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Select(x => x.ToString());
        }
    }
}
