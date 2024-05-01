//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v10.8.5+7e1d1a1
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
	/// <summary>News and Insights Landing Page</summary>
	[PublishedModel("NewsAndInsightsLandingPage")]
	public partial class NewsAndInsightsLandingPage : PublishedContentModel, IAlertComposition, INavigationSettingsComposition, IPageHeroComposition, IPageSpecificIncludesComposition, ISeoComposition
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		public new const string ModelTypeAlias = "NewsAndInsightsLandingPage";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<NewsAndInsightsLandingPage, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public NewsAndInsightsLandingPage(IPublishedContent content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Articles Per Page: Determines how many articles should be shown per page by default.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[ImplementPropertyType("articlesPerPage")]
		public virtual decimal ArticlesPerPage => this.Value<decimal>(_publishedValueFallback, "articlesPerPage");

		///<summary>
		/// Preamble: Introductory content that appears before the article listing.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("preamble")]
		public virtual global::Umbraco.Cms.Core.Strings.IHtmlEncodedString Preamble => this.Value<global::Umbraco.Cms.Core.Strings.IHtmlEncodedString>(_publishedValueFallback, "preamble");

		///<summary>
		/// Alert Content: When populated this will show at the top of page body
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("pageSpecificAlertContent")]
		public virtual global::Umbraco.Cms.Core.Strings.IHtmlEncodedString PageSpecificAlertContent => global::JNCC.PublicWebsite.Core.Models.AlertComposition.GetPageSpecificAlertContent(this, _publishedValueFallback);

		///<summary>
		/// Hide from Navigation: Hides the page from the main navigation.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[ImplementPropertyType("umbracoNaviHide")]
		public virtual bool UmbracoNaviHide => global::JNCC.PublicWebsite.Core.Models.NavigationSettingsComposition.GetUmbracoNaviHide(this, _publishedValueFallback);

		///<summary>
		/// Hide Children from Navigation: Hides any child pages from the main navigation.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[ImplementPropertyType("umbracoNaviHideChildren")]
		public virtual bool UmbracoNaviHideChildren => global::JNCC.PublicWebsite.Core.Models.NavigationSettingsComposition.GetUmbracoNaviHideChildren(this, _publishedValueFallback);

		///<summary>
		/// Headline: A headline that appears above the main content of the page.If no value is provided the page name will be used instead.If a hero image is also provided then this headline appears over the hero image. Otherwise it appears just above the main content.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("headline")]
		public virtual string Headline => global::JNCC.PublicWebsite.Core.Models.PageHeroComposition.GetHeadline(this, _publishedValueFallback);

		///<summary>
		/// Hero Image: The hero image which is displayed above the main content of the page.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("heroImage")]
		public virtual global::Umbraco.Cms.Core.Models.MediaWithCrops HeroImage => global::JNCC.PublicWebsite.Core.Models.PageHeroComposition.GetHeroImage(this, _publishedValueFallback);

		///<summary>
		/// HTML Lang Ref: This field should be used when the page is of another language. When a value is not present in this field, the default value will be "en-GB".
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("hTMLLangRef")]
		public virtual string HTmllangRef => global::JNCC.PublicWebsite.Core.Models.PageSpecificIncludesComposition.GetHTmllangRef(this, _publishedValueFallback);

		///<summary>
		/// Page-specific BODY Includes: Authored code includes which will only appear on this page and will be rendered at the end of the BODY tag in the HTML.This is useful for adding tracking code. Styling should not be authored here and should instead be authored in the head.This should be edited by administrators only.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("pageSpecificBodyIncludes")]
		public virtual string PageSpecificBodyIncludes => global::JNCC.PublicWebsite.Core.Models.PageSpecificIncludesComposition.GetPageSpecificBodyIncludes(this, _publishedValueFallback);

		///<summary>
		/// Page-specific HEAD Includes: Authored code includes which will only appear on this page and will be rendered at the end of the HEAD tag in the HTML.This is useful for adding tracking code and style elements.This should be edited by administrators only.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("pageSpecificHeadIncludes")]
		public virtual string PageSpecificHeadIncludes => global::JNCC.PublicWebsite.Core.Models.PageSpecificIncludesComposition.GetPageSpecificHeadIncludes(this, _publishedValueFallback);

		///<summary>
		/// NoIndex: The default value for this is False, if the checkbox is set to true the NoIndex property will be added to this page
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[ImplementPropertyType("noIndex")]
		public virtual bool NoIndex => global::JNCC.PublicWebsite.Core.Models.SeoComposition.GetNoIndex(this, _publishedValueFallback);

		///<summary>
		/// SEO Settings
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("seoSettings")]
		public virtual global::SEOChecker.Library.Models.MetaData SeoSettings => global::JNCC.PublicWebsite.Core.Models.SeoComposition.GetSeoSettings(this, _publishedValueFallback);
	}
}
