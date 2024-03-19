﻿using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Extensions
{
    public static class PublishedContentExtensions
    {
        public static bool AreChildrenVisible(this IPublishedContent content)
        {
            return content.Value<bool>("umbracoNaviHideChildren") == false;
        }

        //public static T Site<T>(this IPublishedContent content) where T : class, IPublishedContent
        //{
        //    var site = content.Site();

        //    return site as T;
        //}

        public static IEnumerable<IPublishedContent> VisibleChildren(this IPublishedContent content)
        {
            return content.Children.Where(x => x.IsVisible());
        }

        public static TOutput GetPropertyValueFirstOfTypeOrDefault<TEnumerable, TOutput>(this IPublishedContent content, string propertyAlias) where TOutput : TEnumerable
        {
            var values = content.Value<IEnumerable<TEnumerable>>(propertyAlias);

            if (values == null || values.Any() == false)
            {
                return default(TOutput);
            }

            return values.OfType<TOutput>()
                         .FirstOrDefault();
        }

        public static T GetPropertyValueFirstOfTypeOrDefault<T>(this IPublishedContent content, string propertyAlias) where T : IPublishedContent 
        {
            return content.GetPropertyValueFirstOfTypeOrDefault<IPublishedContent, T>(propertyAlias);
        }

        public static IEnumerable<TOutput> GetPropertyValueOfTypeOrDefault<TEnumerable, TOutput>(this IPublishedContent content, string propertyAlias) where TOutput : TEnumerable
        {
            var values = content.Value<IEnumerable<TEnumerable>>(propertyAlias);

            if (values == null || values.Any() == false)
            {
                return default(IEnumerable<TOutput>);
            }

            return values.OfType<TOutput>();
        }

        public static IEnumerable<T> GetPropertyValueOfTypeOrDefault<T>(this IPublishedContent content, string propertyAlias) where T : IPublishedContent
        {
            return content.GetPropertyValueOfTypeOrDefault<IPublishedContent, T>(propertyAlias);
        }
    }
}
