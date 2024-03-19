//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v10.8.4+cbf9f9b
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
	// Mixin Content Type with alias "seoComposition"
	/// <summary>SEO Composition</summary>
	public partial interface ISeoComposition : IPublishedContent
	{
		/// <summary>NoIndex</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.4+cbf9f9b")]
		bool NoIndex { get; }

		/// <summary>SEO Settings</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.4+cbf9f9b")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::SEOChecker.Library.Models.MetaData SeoSettings { get; }
	}

	/// <summary>SEO Composition</summary>
	[PublishedModel("seoComposition")]
	public partial class SeoComposition : PublishedContentModel, ISeoComposition
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.4+cbf9f9b")]
		public new const string ModelTypeAlias = "seoComposition";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.4+cbf9f9b")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.4+cbf9f9b")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.4+cbf9f9b")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<SeoComposition, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public SeoComposition(IPublishedContent content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// NoIndex: The default value for this is False, if the checkbox is set to true the NoIndex property will be added to this page
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.4+cbf9f9b")]
		[ImplementPropertyType("noIndex")]
		public virtual bool NoIndex => GetNoIndex(this, _publishedValueFallback);

		/// <summary>Static getter for NoIndex</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.4+cbf9f9b")]
		public static bool GetNoIndex(ISeoComposition that, IPublishedValueFallback publishedValueFallback) => that.Value<bool>(publishedValueFallback, "noIndex");

		///<summary>
		/// SEO Settings
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.4+cbf9f9b")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("seoSettings")]
		public virtual global::SEOChecker.Library.Models.MetaData SeoSettings => GetSeoSettings(this, _publishedValueFallback);

		/// <summary>Static getter for SEO Settings</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.4+cbf9f9b")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::SEOChecker.Library.Models.MetaData GetSeoSettings(ISeoComposition that, IPublishedValueFallback publishedValueFallback) => that.Value<global::SEOChecker.Library.Models.MetaData>(publishedValueFallback, "seoSettings");
	}
}
