//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb
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
	/// <summary>iFrame Page</summary>
	[PublishedModel("iFramePage")]
	public partial class IFramePage : PublishedContentModel, INavigationSettingsComposition, IPageSpecificIncludesComposition, ISeoComposition
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		public new const string ModelTypeAlias = "iFramePage";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<IFramePage, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public IFramePage(IPublishedContent content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Cookie Disabled Error: This is the message displayed if the user has not accepted the cookies on the website
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("cookieDisabledError")]
		public virtual global::Umbraco.Cms.Core.Strings.IHtmlEncodedString CookieDisabledError => this.Value<global::Umbraco.Cms.Core.Strings.IHtmlEncodedString>(_publishedValueFallback, "cookieDisabledError");

		///<summary>
		/// Display on Medium Devices ?: Set to true, to display above fallback content on medium devices (Tablet)
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[ImplementPropertyType("displayOnMediumDevices")]
		public virtual bool DisplayOnMediumDevices => this.Value<bool>(_publishedValueFallback, "displayOnMediumDevices");

		///<summary>
		/// Display on Small Devices ?: Set to true, to display above fallback content on small devices (Mobile)
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[ImplementPropertyType("displayOnSmallDevices")]
		public virtual bool DisplayOnSmallDevices => this.Value<bool>(_publishedValueFallback, "displayOnSmallDevices");

		///<summary>
		/// Fallback Content: This content is a text alternative for the mapper page, this will be displayed on the devices set by the toggle switches below
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("fallbackContent")]
		public virtual global::Umbraco.Cms.Core.Models.Blocks.BlockListModel FallbackContent => this.Value<global::Umbraco.Cms.Core.Models.Blocks.BlockListModel>(_publishedValueFallback, "fallbackContent");

		///<summary>
		/// Navigation: An optional collection of navigation links which is independent of the main navigation and will only appear on this iFrame page.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("navigation")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.Link> Navigation => this.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.Link>>(_publishedValueFallback, "navigation");

		///<summary>
		/// Passthrough Querystring Parameters: Allows any querystrings being passed in via the URL to be appended to the source URL of the the iframe.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[ImplementPropertyType("passthroughQuerystringParameters")]
		public virtual bool PassthroughQuerystringParameters => this.Value<bool>(_publishedValueFallback, "passthroughQuerystringParameters");

		///<summary>
		/// Source URL: The URL that displays within the iFrame.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("sourceURL")]
		public virtual string SourceUrl => this.Value<string>(_publishedValueFallback, "sourceURL");

		///<summary>
		/// Hide from Navigation: Hides the page from the main navigation.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[ImplementPropertyType("umbracoNaviHide")]
		public virtual bool UmbracoNaviHide => global::JNCC.PublicWebsite.Core.Models.NavigationSettingsComposition.GetUmbracoNaviHide(this, _publishedValueFallback);

		///<summary>
		/// Hide Children from Navigation: Hides any child pages from the main navigation.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[ImplementPropertyType("umbracoNaviHideChildren")]
		public virtual bool UmbracoNaviHideChildren => global::JNCC.PublicWebsite.Core.Models.NavigationSettingsComposition.GetUmbracoNaviHideChildren(this, _publishedValueFallback);

		///<summary>
		/// HTML Lang Ref: This field should be used when the page is of another language. When a value is not present in this field, the default value will be "en-GB".
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("hTMLLangRef")]
		public virtual string HTmllangRef => global::JNCC.PublicWebsite.Core.Models.PageSpecificIncludesComposition.GetHTmllangRef(this, _publishedValueFallback);

		///<summary>
		/// Page-specific BODY Includes: Authored code includes which will only appear on this page and will be rendered at the end of the BODY tag in the HTML.This is useful for adding tracking code. Styling should not be authored here and should instead be authored in the head.This should be edited by administrators only.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("pageSpecificBodyIncludes")]
		public virtual string PageSpecificBodyIncludes => global::JNCC.PublicWebsite.Core.Models.PageSpecificIncludesComposition.GetPageSpecificBodyIncludes(this, _publishedValueFallback);

		///<summary>
		/// Page-specific HEAD Includes: Authored code includes which will only appear on this page and will be rendered at the end of the HEAD tag in the HTML.This is useful for adding tracking code and style elements.This should be edited by administrators only.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("pageSpecificHeadIncludes")]
		public virtual string PageSpecificHeadIncludes => global::JNCC.PublicWebsite.Core.Models.PageSpecificIncludesComposition.GetPageSpecificHeadIncludes(this, _publishedValueFallback);

		///<summary>
		/// NoIndex: The default value for this is False, if the checkbox is set to true the NoIndex property will be added to this page
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[ImplementPropertyType("noIndex")]
		public virtual bool NoIndex => global::JNCC.PublicWebsite.Core.Models.SeoComposition.GetNoIndex(this, _publishedValueFallback);

		///<summary>
		/// SEO Settings
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("seoSettings")]
		public virtual global::SEOChecker.Library.Models.MetaData SeoSettings => global::JNCC.PublicWebsite.Core.Models.SeoComposition.GetSeoSettings(this, _publishedValueFallback);
	}
}
