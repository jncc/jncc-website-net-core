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
	/// <summary>Content Item Schema</summary>
	[PublishedModel("contentItemSchema")]
	public partial class ContentItemSchema : PublishedElementModel
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		public new const string ModelTypeAlias = "contentItemSchema";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<ContentItemSchema, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public ContentItemSchema(IPublishedElement content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Content: A brief paragraph or two explaining this content item.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("content")]
		public virtual global::Umbraco.Cms.Core.Strings.IHtmlEncodedString Content => this.Value<global::Umbraco.Cms.Core.Strings.IHtmlEncodedString>(_publishedValueFallback, "content");

		///<summary>
		/// Get in Touch Button: The link & text for the button that links to a full web page for getting in touch about this content item.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("getInTouchButton")]
		public virtual global::Umbraco.Cms.Core.Models.Link GetInTouchButton => this.Value<global::Umbraco.Cms.Core.Models.Link>(_publishedValueFallback, "getInTouchButton");

		///<summary>
		/// Image
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("image")]
		public virtual global::Umbraco.Cms.Core.Models.Blocks.BlockListModel Image => this.Value<global::Umbraco.Cms.Core.Models.Blocks.BlockListModel>(_publishedValueFallback, "image");

		///<summary>
		/// Read More Button: The link & text for the button that links to the full web page for this content item.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.5+7e1d1a1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("readMoreButton")]
		public virtual global::Umbraco.Cms.Core.Models.Link ReadMoreButton => this.Value<global::Umbraco.Cms.Core.Models.Link>(_publishedValueFallback, "readMoreButton");
	}
}
