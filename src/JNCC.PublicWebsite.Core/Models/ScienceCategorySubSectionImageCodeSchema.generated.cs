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
	/// <summary>Science Category Sub Section Image Code Schema</summary>
	[PublishedModel("scienceCategorySubSectionImageCodeSchema")]
	public partial class ScienceCategorySubSectionImageCodeSchema : ScienceCategorySectionBaseSchema
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		public new const string ModelTypeAlias = "scienceCategorySubSectionImageCodeSchema";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<ScienceCategorySubSectionImageCodeSchema, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public ScienceCategorySubSectionImageCodeSchema(IPublishedElement content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Content
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("content")]
		public virtual global::Umbraco.Cms.Core.Strings.IHtmlEncodedString Content => this.Value<global::Umbraco.Cms.Core.Strings.IHtmlEncodedString>(_publishedValueFallback, "content");

		///<summary>
		/// Image Code
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("imageCode")]
		public virtual string ImageCode => this.Value<string>(_publishedValueFallback, "imageCode");

		///<summary>
		/// Image Position: Accepted written positions are: 'Left', 'Right', 'Top'
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("imagePosition")]
		public virtual string ImagePosition => this.Value<string>(_publishedValueFallback, "imagePosition");

		///<summary>
		/// Sub Sections
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.5.4+4df082703500212e5bf2ded279a06d5bbffb212c")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("subSections")]
		public virtual global::Umbraco.Cms.Core.Models.Blocks.BlockListModel SubSections => this.Value<global::Umbraco.Cms.Core.Models.Blocks.BlockListModel>(_publishedValueFallback, "subSections");
	}
}
