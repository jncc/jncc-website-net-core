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
	/// <summary>Articulate</summary>
	[PublishedModel("Articulate")]
	public partial class Articulate : PublishedContentModel
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		public new const string ModelTypeAlias = "Articulate";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<Articulate, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public Articulate(IPublishedContent content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Blog Banner: Optional blog banner
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("blogBanner")]
		public virtual global::Umbraco.Cms.Core.PropertyEditors.ValueConverters.ImageCropperValue BlogBanner => this.Value<global::Umbraco.Cms.Core.PropertyEditors.ValueConverters.ImageCropperValue>(_publishedValueFallback, "blogBanner");

		///<summary>
		/// Blog Description
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("blogDescription")]
		public virtual string BlogDescription => this.Value<string>(_publishedValueFallback, "blogDescription");

		///<summary>
		/// Blog Logo: Optional logo
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("blogLogo")]
		public virtual global::Umbraco.Cms.Core.PropertyEditors.ValueConverters.ImageCropperValue BlogLogo => this.Value<global::Umbraco.Cms.Core.PropertyEditors.ValueConverters.ImageCropperValue>(_publishedValueFallback, "blogLogo");

		///<summary>
		/// Blog Title
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("blogTitle")]
		public virtual string BlogTitle => this.Value<string>(_publishedValueFallback, "blogTitle");

		///<summary>
		/// Categories Page Name: The page title for the categories listing
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("categoriesPageName")]
		public virtual string CategoriesPageName => this.Value<string>(_publishedValueFallback, "categoriesPageName");

		///<summary>
		/// Categories Url Name
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("categoriesUrlName")]
		public virtual string CategoriesUrlName => this.Value<string>(_publishedValueFallback, "categoriesUrlName");

		///<summary>
		/// Comments Form: The Umbraco Form used by website users to submit comments to blog posts in this blog. If no form is selected users will not be able to submit comments.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("commentsForm")]
		public virtual string CommentsForm => this.Value<string>(_publishedValueFallback, "commentsForm");

		///<summary>
		/// Custom RSS Feed Url: Optional custom rss feed URL (i.e. if you use feedburner, etc...)
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("customRssFeedUrl")]
		public virtual string CustomRssFeedUrl => this.Value<string>(_publishedValueFallback, "customRssFeedUrl");

		///<summary>
		/// Disqus Shortname
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("disqusShortname")]
		public virtual string DisqusShortname => this.Value<string>(_publishedValueFallback, "disqusShortname");

		///<summary>
		/// Enable Comments: If this is disabled then comments will not be allowed within this blog regardless of individual blog post settings.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[ImplementPropertyType("enableComments")]
		public virtual bool EnableComments => this.Value<bool>(_publishedValueFallback, "enableComments");

		///<summary>
		/// Extract First Image to Property: When Windows Live Writer (or compatible WebBlog API tool) is used to create blog posts, with this option enabled it will set the first image found in the blog post to the blog's image property if one is found.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[ImplementPropertyType("extractFirstImage")]
		public virtual bool ExtractFirstImage => this.Value<bool>(_publishedValueFallback, "extractFirstImage");

		///<summary>
		/// Google Analytics Id: Your Google Analytics Id (i.e. UA-123456789 )
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("googleAnalyticsId")]
		public virtual string GoogleAnalyticsId => this.Value<string>(_publishedValueFallback, "googleAnalyticsId");

		///<summary>
		/// Google Analytics Name: The site name associated with your Google Analytics (i.e. mysite.com )
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("googleAnalyticsName")]
		public virtual string GoogleAnalyticsName => this.Value<string>(_publishedValueFallback, "googleAnalyticsName");

		///<summary>
		/// PageSize
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[ImplementPropertyType("pageSize")]
		public virtual int PageSize => this.Value<int>(_publishedValueFallback, "pageSize");

		///<summary>
		/// Redirect Archive: If specified this will redirect the Archive blog post container URL to this Articulate blog root
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[ImplementPropertyType("redirectArchive")]
		public virtual bool RedirectArchive => this.Value<bool>(_publishedValueFallback, "redirectArchive");

		///<summary>
		/// Search Page Name: The page title for the search results
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("searchPageName")]
		public virtual string SearchPageName => this.Value<string>(_publishedValueFallback, "searchPageName");

		///<summary>
		/// Search Url Name
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("searchUrlName")]
		public virtual string SearchUrlName => this.Value<string>(_publishedValueFallback, "searchUrlName");

		///<summary>
		/// Tags Page Name: The page title for the tags listing
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("tagsPageName")]
		public virtual string TagsPageName => this.Value<string>(_publishedValueFallback, "tagsPageName");

		///<summary>
		/// Tags Url Name
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("tagsUrlName")]
		public virtual string TagsUrlName => this.Value<string>(_publishedValueFallback, "tagsUrlName");

		///<summary>
		/// Theme
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("theme")]
		public virtual string Theme => this.Value<string>(_publishedValueFallback, "theme");

		///<summary>
		/// Use yyyy/mm/dd format for Url: If specified, this will generate posts' urls in the /yyyy/mm/dd/slug format, ie 2017/06/09/codegarden-rocks
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.2.0+763cb70e677ac0c85557b19b5df09eccfa1b9dfb")]
		[ImplementPropertyType("useDateFormatForUrl")]
		public virtual bool UseDateFormatForUrl => this.Value<bool>(_publishedValueFallback, "useDateFormatForUrl");
	}
}
