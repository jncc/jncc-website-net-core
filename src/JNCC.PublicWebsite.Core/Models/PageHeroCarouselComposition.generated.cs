//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Infrastructure.ModelsBuilder;
using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Models
{
	// Mixin Content Type with alias "pageHeroCarouselComposition"
	/// <summary>Page Hero Carousel Composition</summary>
	public partial interface IPageHeroCarouselComposition : IPublishedContent
	{
		/// <summary>Headline</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		string Headline { get; }

		/// <summary>Hero Content</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::Umbraco.Cms.Core.Strings.IHtmlEncodedString HeroContent { get; }

		/// <summary>Hero Images</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.MediaWithCrops> HeroImages { get; }
	}

	/// <summary>Page Hero Carousel Composition</summary>
	[PublishedModel("pageHeroCarouselComposition")]
	public partial class PageHeroCarouselComposition : PublishedContentModel, IPageHeroCarouselComposition
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		public new const string ModelTypeAlias = "pageHeroCarouselComposition";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<PageHeroCarouselComposition, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public PageHeroCarouselComposition(IPublishedContent content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Headline: A headline which appears within the Hero Carousel.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("headline")]
		public virtual string Headline => GetHeadline(this, _publishedValueFallback);

		/// <summary>Static getter for Headline</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static string GetHeadline(IPageHeroCarouselComposition that, IPublishedValueFallback publishedValueFallback) => that.Value<string>(publishedValueFallback, "headline");

		///<summary>
		/// Hero Content: A brief paragraph to be displayed within the hero carousel.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("heroContent")]
		public virtual global::Umbraco.Cms.Core.Strings.IHtmlEncodedString HeroContent => GetHeroContent(this, _publishedValueFallback);

		/// <summary>Static getter for Hero Content</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::Umbraco.Cms.Core.Strings.IHtmlEncodedString GetHeroContent(IPageHeroCarouselComposition that, IPublishedValueFallback publishedValueFallback) => that.Value<global::Umbraco.Cms.Core.Strings.IHtmlEncodedString>(publishedValueFallback, "heroContent");

		///<summary>
		/// Hero Images: Images to be displayed in the hero image carousel
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("heroImages")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.MediaWithCrops> HeroImages => GetHeroImages(this, _publishedValueFallback);

		/// <summary>Static getter for Hero Images</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.MediaWithCrops> GetHeroImages(IPageHeroCarouselComposition that, IPublishedValueFallback publishedValueFallback) => that.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.MediaWithCrops>>(publishedValueFallback, "heroImages");
	}
}
