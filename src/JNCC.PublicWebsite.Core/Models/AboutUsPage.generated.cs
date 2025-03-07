//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v10.8.7+35c51a0
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
	/// <summary>About Us Page</summary>
	[PublishedModel("aboutUsPage")]
	public partial class AboutUsPage : PublishedContentModel, IAlertComposition, IGetInTouchComposition, INavigationSettingsComposition, IPageHeroComposition, IPageMetaInformationComposition, IPageSpecificIncludesComposition, IRelatedItemsComposition, ISeoComposition, ISidebarComposition
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		public new const string ModelTypeAlias = "aboutUsPage";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<AboutUsPage, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public AboutUsPage(IPublishedContent content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Main Content: The main content of the page.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("mainContent")]
		public virtual global::Umbraco.Cms.Core.Models.Blocks.BlockListModel MainContent => this.Value<global::Umbraco.Cms.Core.Models.Blocks.BlockListModel>(_publishedValueFallback, "mainContent");

		///<summary>
		/// Preamble: Introductory content regards JNCC.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("preamble")]
		public virtual global::Umbraco.Cms.Core.Strings.IHtmlEncodedString Preamble => this.Value<global::Umbraco.Cms.Core.Strings.IHtmlEncodedString>(_publishedValueFallback, "preamble");

		///<summary>
		/// Alert Content: When populated this will show at the top of page body
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("pageSpecificAlertContent")]
		public virtual global::Umbraco.Cms.Core.Strings.IHtmlEncodedString PageSpecificAlertContent => global::JNCC.PublicWebsite.Core.Models.AlertComposition.GetPageSpecificAlertContent(this, _publishedValueFallback);

		///<summary>
		/// Get in Touch Button: The link & text for the Get in Touch button which accompanies the Get in Touch content below the main content of the page.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("getInTouchButton")]
		public virtual global::Umbraco.Cms.Core.Models.Link GetInTouchButton => global::JNCC.PublicWebsite.Core.Models.GetInTouchComposition.GetGetInTouchButton(this, _publishedValueFallback);

		///<summary>
		/// Get In Touch Content: Optional content which appears below the main content of the page. This content is specifically for encouraging website users to navigate to the contact form.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("getInTouchContent")]
		public virtual global::Umbraco.Cms.Core.Strings.IHtmlEncodedString GetInTouchContent => global::JNCC.PublicWebsite.Core.Models.GetInTouchComposition.GetGetInTouchContent(this, _publishedValueFallback);

		///<summary>
		/// Hide from Navigation: Hides the page from the main navigation.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[ImplementPropertyType("umbracoNaviHide")]
		public virtual bool UmbracoNaviHide => global::JNCC.PublicWebsite.Core.Models.NavigationSettingsComposition.GetUmbracoNaviHide(this, _publishedValueFallback);

		///<summary>
		/// Hide Children from Navigation: Hides any child pages from the main navigation.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[ImplementPropertyType("umbracoNaviHideChildren")]
		public virtual bool UmbracoNaviHideChildren => global::JNCC.PublicWebsite.Core.Models.NavigationSettingsComposition.GetUmbracoNaviHideChildren(this, _publishedValueFallback);

		///<summary>
		/// Headline: A headline that appears above the main content of the page.If no value is provided the page name will be used instead.If a hero image is also provided then this headline appears over the hero image. Otherwise it appears just above the main content.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("headline")]
		public virtual string Headline => global::JNCC.PublicWebsite.Core.Models.PageHeroComposition.GetHeadline(this, _publishedValueFallback);

		///<summary>
		/// Hero Image: The hero image which is displayed above the main content of the page.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("heroImage")]
		public virtual global::Umbraco.Cms.Core.Models.MediaWithCrops HeroImage => global::JNCC.PublicWebsite.Core.Models.PageHeroComposition.GetHeroImage(this, _publishedValueFallback);

		///<summary>
		/// Published Date: The date is when the page was first published. This is a required property as a page with a Meta Information must have a published date.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[ImplementPropertyType("publishedDate")]
		public virtual global::System.DateTime PublishedDate => global::JNCC.PublicWebsite.Core.Models.PageMetaInformationComposition.GetPublishedDate(this, _publishedValueFallback);

		///<summary>
		/// HTML Lang Ref: This field should be used when the page is of another language. When a value is not present in this field, the default value will be "en-GB".
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("hTMLLangRef")]
		public virtual string HTmllangRef => global::JNCC.PublicWebsite.Core.Models.PageSpecificIncludesComposition.GetHTmllangRef(this, _publishedValueFallback);

		///<summary>
		/// Page-specific BODY Includes: Authored code includes which will only appear on this page and will be rendered at the end of the BODY tag in the HTML.This is useful for adding tracking code. Styling should not be authored here and should instead be authored in the head.This should be edited by administrators only.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("pageSpecificBodyIncludes")]
		public virtual string PageSpecificBodyIncludes => global::JNCC.PublicWebsite.Core.Models.PageSpecificIncludesComposition.GetPageSpecificBodyIncludes(this, _publishedValueFallback);

		///<summary>
		/// Page-specific HEAD Includes: Authored code includes which will only appear on this page and will be rendered at the end of the HEAD tag in the HTML.This is useful for adding tracking code and style elements.This should be edited by administrators only.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("pageSpecificHeadIncludes")]
		public virtual string PageSpecificHeadIncludes => global::JNCC.PublicWebsite.Core.Models.PageSpecificIncludesComposition.GetPageSpecificHeadIncludes(this, _publishedValueFallback);

		///<summary>
		/// Items: Provides related items for the current page. These items are manually authored. A maximum of 3 items can be authored.If less than 3 items are provided then a query will be made to find the remaining items needed.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("relatedItems")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> RelatedItems => global::JNCC.PublicWebsite.Core.Models.RelatedItemsComposition.GetRelatedItems(this, _publishedValueFallback);

		///<summary>
		/// Show Related Items
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[ImplementPropertyType("showRelatedItems")]
		public virtual bool ShowRelatedItems => global::JNCC.PublicWebsite.Core.Models.RelatedItemsComposition.GetShowRelatedItems(this, _publishedValueFallback);

		///<summary>
		/// NoIndex: The default value for this is False, if the checkbox is set to true the NoIndex property will be added to this page
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[ImplementPropertyType("noIndex")]
		public virtual bool NoIndex => global::JNCC.PublicWebsite.Core.Models.SeoComposition.GetNoIndex(this, _publishedValueFallback);

		///<summary>
		/// SEO Settings
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("seoSettings")]
		public virtual global::SEOChecker.Library.Models.MetaData SeoSettings => global::JNCC.PublicWebsite.Core.Models.SeoComposition.GetSeoSettings(this, _publishedValueFallback);

		///<summary>
		/// Elsewhere on our website links: Links to other internal web pages
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("sidebarElsewhereOnOurWebsite")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.Link> SidebarElsewhereOnOurWebsite => global::JNCC.PublicWebsite.Core.Models.SidebarComposition.GetSidebarElsewhereOnOurWebsite(this, _publishedValueFallback);

		///<summary>
		/// Other websites: Links to other external web pages
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("sidebarOtherWebsites")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.Link> SidebarOtherWebsites => global::JNCC.PublicWebsite.Core.Models.SidebarComposition.GetSidebarOtherWebsites(this, _publishedValueFallback);

		///<summary>
		/// Primary Call To Action Button: Link & Text for an optional Call to Action button.This could be various purposes, for example "Get in Touch" or "Download Data".
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("sidebarPrimaryCallToActionButton")]
		public virtual global::Umbraco.Cms.Core.Models.Link SidebarPrimaryCallToActionButton => global::JNCC.PublicWebsite.Core.Models.SidebarComposition.GetSidebarPrimaryCallToActionButton(this, _publishedValueFallback);

		///<summary>
		/// See Also Links: Useful links to other internal & external web pages.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.7+35c51a0")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("sidebarSeeAlsoLinks")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.Link> SidebarSeeAlsoLinks => global::JNCC.PublicWebsite.Core.Models.SidebarComposition.GetSidebarSeeAlsoLinks(this, _publishedValueFallback);
	}
}
