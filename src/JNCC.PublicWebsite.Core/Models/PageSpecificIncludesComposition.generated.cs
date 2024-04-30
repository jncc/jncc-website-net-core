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
	// Mixin Content Type with alias "pageSpecificIncludesComposition"
	/// <summary>Page-Specific Includes Composition</summary>
	public partial interface IPageSpecificIncludesComposition : IPublishedContent
	{
		/// <summary>HTML Lang Ref</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		string HTmllangRef { get; }

		/// <summary>Page-specific BODY Includes</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		string PageSpecificBodyIncludes { get; }

		/// <summary>Page-specific HEAD Includes</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		string PageSpecificHeadIncludes { get; }
	}

	/// <summary>Page-Specific Includes Composition</summary>
	[PublishedModel("pageSpecificIncludesComposition")]
	public partial class PageSpecificIncludesComposition : PublishedContentModel, IPageSpecificIncludesComposition
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		public new const string ModelTypeAlias = "pageSpecificIncludesComposition";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<PageSpecificIncludesComposition, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public PageSpecificIncludesComposition(IPublishedContent content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// HTML Lang Ref: This field should be used when the page is of another language. When a value is not present in this field, the default value will be "en-GB".
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("hTMLLangRef")]
		public virtual string HTmllangRef => GetHTmllangRef(this, _publishedValueFallback);

		/// <summary>Static getter for HTML Lang Ref</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static string GetHTmllangRef(IPageSpecificIncludesComposition that, IPublishedValueFallback publishedValueFallback) => that.Value<string>(publishedValueFallback, "hTMLLangRef");

		///<summary>
		/// Page-specific BODY Includes: Authored code includes which will only appear on this page and will be rendered at the end of the BODY tag in the HTML.This is useful for adding tracking code. Styling should not be authored here and should instead be authored in the head.This should be edited by administrators only.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("pageSpecificBodyIncludes")]
		public virtual string PageSpecificBodyIncludes => GetPageSpecificBodyIncludes(this, _publishedValueFallback);

		/// <summary>Static getter for Page-specific BODY Includes</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static string GetPageSpecificBodyIncludes(IPageSpecificIncludesComposition that, IPublishedValueFallback publishedValueFallback) => that.Value<string>(publishedValueFallback, "pageSpecificBodyIncludes");

		///<summary>
		/// Page-specific HEAD Includes: Authored code includes which will only appear on this page and will be rendered at the end of the HEAD tag in the HTML.This is useful for adding tracking code and style elements.This should be edited by administrators only.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("pageSpecificHeadIncludes")]
		public virtual string PageSpecificHeadIncludes => GetPageSpecificHeadIncludes(this, _publishedValueFallback);

		/// <summary>Static getter for Page-specific HEAD Includes</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static string GetPageSpecificHeadIncludes(IPageSpecificIncludesComposition that, IPublishedValueFallback publishedValueFallback) => that.Value<string>(publishedValueFallback, "pageSpecificHeadIncludes");
	}
}
