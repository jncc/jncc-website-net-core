using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Interfaces.Providers;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Strings;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ArticlesPageService : IArticlesPageService
    {
        private readonly INavigationItemService _navigationItemService;
        private readonly IShortStringHelper _shortStringHelper;
        public ArticlesPageService(INavigationItemService navigationItemService, IShortStringHelper shortStringHelper)
        {
            _navigationItemService = navigationItemService ?? throw new ArgumentNullException(nameof(navigationItemService));
            _shortStringHelper = shortStringHelper ?? throw new ArgumentNullException(nameof(shortStringHelper));
        }

        public IEnumerable<ArticlesSectionViewModel> GetSectionViewModels(BlockListModel mainContent)
        {
            var viewModels = new List<ArticlesSectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(mainContent))
            {
                return viewModels;
            }
            foreach (var section in mainContent)
            {
                ArticlesSectionViewModel viewModel = null;

                switch (section.Content)
                {
                    case ArticlesSectionRichTextSchema richText:
                        viewModel = CreateRichTextSection(richText);
                        break;
                    case ArticlesSectionImageGallerySchema imageGallery:
                        viewModel = CreateImageGallerySection(imageGallery);
                        break;
                    case ArticlesSectionImageTextSchema imageRichText:
                        viewModel = CreateImageRichTextSection(imageRichText);
                        break;
                    case ArticlesSectionImageCodeSchema imageCode:
                        viewModel = CreateImageCodeSection(imageCode);
                        break;
                    case ArticlesSectionSliderSchema slider:
                        viewModel = CreateSliderSection(slider);
                        break;
                }

                if (viewModel != null)
                {
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }


        private TViewModel CreateSection<TViewModel>(ArticlesSectionBaseSchema schema, string parentSectionHtmlId = null) where TViewModel : ArticlesSectionViewModelBase, new()
        {
            var section = new TViewModel()
            {
                Headline = schema.Headline,
                PartialViewName = GetPartialViewName(schema),
                HideHeadline = schema.HideHeadline
            };

            var sectionHtmlId = schema.Headline.ToUrlSegment(_shortStringHelper);

            if (string.IsNullOrWhiteSpace(parentSectionHtmlId))
            {
                section.HtmlId = sectionHtmlId;
            }
            else
            {
                section.HtmlId = string.Join("-", parentSectionHtmlId, sectionHtmlId);
            }


            return section;
        }

        private string GetPartialViewName(ArticlesSectionBaseSchema schema)
        {
            switch (schema.ContentType.Alias)
            {
                case ArticlesSectionRichTextSchema.ModelTypeAlias:
                case ArticlesSubSectionRichTextSchema.ModelTypeAlias:
                    return ArticlesPartialViewNames.RichText;
                case ArticlesSubSectionImageGallerySchema.ModelTypeAlias:
                case ArticlesSectionImageGallerySchema.ModelTypeAlias:
                    return ArticlesPartialViewNames.ImageGallery;
               
                case ArticlesSectionImageTextSchema.ModelTypeAlias:
                case ArticlesSubSectionImageRichTextSchema.ModelTypeAlias:
                    return ArticlesPartialViewNames.ImageRichText;
               
                case ArticlesSectionImageCodeSchema.ModelTypeAlias:
                case ArticlesSubSectionImageCodeSchema.ModelTypeAlias:
                    return ArticlesPartialViewNames.ImageCode;
                case ArticlesSectionSliderSchema.ModelTypeAlias:
                case ArticlesSubSectionSliderSchema.ModelTypeAlias:
                    return ArticlesPartialViewNames.Slider;
                default:
                    throw new NotSupportedException($"Document Type, {schema.ContentType.Alias}, is not currently supported.");
            }

        }



        public ArticlesRichTextSectionViewModel CreateRichTextSection(ArticlesSectionRichTextSchema schema)
        {
            var model = CreateSection<ArticlesRichTextSectionViewModel>(schema);

            model.Content = schema.Content;

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public IEnumerable<ArticlesSubSectionViewModel> GetSubSectionViewModels(BlockListModel subSections, string parentHtmlId)
        {
            var viewModels = new List<ArticlesSubSectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(subSections))
            {
                return viewModels;
            }

            foreach (var section in subSections)
            {
                ArticlesSubSectionViewModel viewModel = null;

                switch (section.Content)
                {
                    case ArticlesSubSectionRichTextSchema richText:
                        viewModel = CreateRichTextSubSection(richText, parentHtmlId);
                        break;
                    case ArticlesSubSectionImageGallerySchema imageGallery:
                        viewModel = CreateImageGallerySubSection(imageGallery, parentHtmlId);
                        break;
                    case ArticlesSubSectionImageRichTextSchema imageRichText:
                        viewModel = CreateImageRichTextSubSection(imageRichText, parentHtmlId);
                        break;
                    case ArticlesSubSectionImageCodeSchema imageCode:
                        viewModel = CreateImageCodeSubSection(imageCode, parentHtmlId);
                        break;
                    case ArticlesSubSectionSliderSchema slider:
                        viewModel = CreateSliderSubSection(slider, parentHtmlId);
                        break;
                }

                if (viewModel != null)
                {
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }



        public ArticlesImageGallerySectionViewModel CreateImageGallerySection(ArticlesSectionImageGallerySchema schema)
        {
            var model = CreateSection<ArticlesImageGallerySectionViewModel>(schema);

            model.Images = CreateSectionImageGallery(schema.Images);

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public IEnumerable<ImageGalleryItemViewModel> CreateSectionImageGallery(IEnumerable<MediaWithCrops> images)
        {
            var viewModels = new List<ImageGalleryItemViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(images))
            {
                return viewModels;
            }

            foreach (MediaWithCrops galleryItem in images)
            {
                if (galleryItem?.Content is Image image)
                {
                    var viewModel = new ImageGalleryItemViewModel()
                    {
                        Url = image.Url(),
                        ThumbnailImageUrl = image.GetCropUrl(cropAlias: ImageCropAliases.Square, urlMode: UrlMode.Absolute),
                        AlternativeText = image.AltText.IsNullOrWhiteSpace() ? image.Name : image.AltText,
                        TitleText = image.TitleText,
                    };

                    if (string.IsNullOrEmpty(image.Url()) == false)
                    {
                        viewModels.Add(viewModel);
                    }
                }
            }

            return viewModels;
        }

        public ArticlesImageRichTextSectionViewModel CreateImageRichTextSection(ArticlesSectionImageTextSchema schema)
        {
            var model = CreateSection<ArticlesImageRichTextSectionViewModel>(schema);

            model.Content = schema.Content;
            if (schema.Image != null)
            {
                if (schema.Image?.Content is Image image)
                {
                    model.Image = new ImageViewModel()
                    {
                        Url = schema.Image.Url(),
                        AlternativeText = image.AltText.IsNullOrWhiteSpace() ? schema.Image.Name : image.AltText,
                        TitleText = image.TitleText,
                    };
                }
            }
            else
            {
                model.Image = new ImageViewModel()
                {
                    Url = null,
                    AlternativeText = null,
                    TitleText = null,
                };
            }
            model.ImagePosition = schema.ImagePosition;

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public ArticlesImageCodeSectionViewModel CreateImageCodeSection(ArticlesSectionImageCodeSchema schema)
        {
            var model = CreateSection<ArticlesImageCodeSectionViewModel>(schema);

            model.Content = schema.Content;
            model.ImageCode = schema.ImageCode;
            model.ImagePosition = schema.ImagePosition;
            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public ArticlesSliderSectionViewModel CreateSliderSection(ArticlesSectionSliderSchema schema)
        {
            var model = CreateSection<ArticlesSliderSectionViewModel>(schema);

            model.Content = schema.Content;
            model.ShowBackground = schema.ShowGreyBackground;
            model.ShowTimelineArrows = schema.ShowTimelineArrows;
            model.SliderItems = GetSliderItemViewModels(schema.SliderItems);
            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public IEnumerable<ScienceSliderSchemaViewModel> GetSliderItemViewModels(BlockListModel sliderItems)
        {
            var viewModels = new List<ScienceSliderSchemaViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(sliderItems))
            {
                return viewModels;
            }

            foreach (var section in sliderItems)
            {
                ScienceSliderSchemaViewModel viewModel = null;

                switch (section.Content)
                {
                    case ScienceSliderSchemaHeadingText headingText:
                        viewModel = CreateHeadingTextSliderItem(headingText);
                        break;
                    case ScienceSliderSchemaImageText imageText:
                        viewModel = CreateImageTextSliderItem(imageText);
                        break;
                }

                if (viewModel != null)
                {
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        public ScienceSliderSchemaViewModel CreateHeadingTextSliderItem(ScienceSliderSchemaHeadingText schema)
        {
            var model = new ScienceSliderSchemaViewModel();

            model.Heading = schema.Heading;
            model.ImageTextSection = false;
            model.Text = schema.Text;

            return model;
        }

        public ScienceSliderSchemaViewModel CreateImageTextSliderItem(ScienceSliderSchemaImageText schema)
        {
            var model = new ScienceSliderSchemaViewModel();

            model.ImageTextSection = true;
            model.Text = schema.Text;
            model.ImagePosition = schema.ImagePosition;

            if (schema.Image != null)
            {
                if (schema.Image?.Content is Image image)
                {
                    model.Image = new ImageViewModel()
                    {
                        Url = schema.Image.Url(),
                        AlternativeText = image.AltText.IsNullOrWhiteSpace() ? schema.Image.Name : image.AltText,
                        TitleText = image.TitleText,
                    };
                }
            }
            else
            {
                model.Image = new ImageViewModel()
                {
                    Url = null,
                    AlternativeText = null,
                    TitleText = null
                };
            }

            if (schema.ImageLink != null)
            {
                model.ImageLink = new NavigationItemViewModel()
                {
                    Url = schema.ImageLink.Url,
                    Text = schema.ImageLink.Name,
                    Target = schema.ImageLink.Target,
                };
            }
            else
            {
                model.ImageLink = new NavigationItemViewModel() { Url = null, Text = null, Target = null };
            }

            return model;
        }


        //Sub Sections
        public  ArticlesRichTextSubSectionViewModel CreateRichTextSubSection(ArticlesSubSectionRichTextSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ArticlesRichTextSubSectionViewModel>(schema, parentSectionHtmlId);
            model.Content = schema.Content;

            model.SubSections = GetSubSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public ArticlesImageGallerySubSectionViewModel CreateImageGallerySubSection(ArticlesSubSectionImageGallerySchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ArticlesImageGallerySubSectionViewModel>(schema, parentSectionHtmlId);

            model.Images = CreateSectionImageGallery(schema.Images);
            model.SubSections = GetSubSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public ArticlesImageRichTextSubSectionViewModel CreateImageRichTextSubSection(ArticlesSubSectionImageRichTextSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ArticlesImageRichTextSubSectionViewModel>(schema, parentSectionHtmlId);

            model.Content = schema.Content;
            if (schema.Image != null)
            {
                if (schema.Image?.Content is Image image)
                {
                    model.Image = new ImageViewModel()
                    {
                        Url = schema.Image.Url(),
                        AlternativeText = image.AltText.IsNullOrWhiteSpace() ? schema.Image.Name : image.AltText,
                        TitleText = image.TitleText,
                    };
                }
            }
            else
            {
                model.Image = new ImageViewModel()
                {
                    Url = null,
                    AlternativeText = null,
                    TitleText = null,
                };
            }
            model.ImagePosition = schema.ImagePosition;

            return model;
        }

        public ArticlesImageCodeSubSectionViewModel CreateImageCodeSubSection(ArticlesSubSectionImageCodeSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ArticlesImageCodeSubSectionViewModel>(schema, parentSectionHtmlId);

            model.Content = schema.Content;
            model.ImageCode = schema.ImageCode;
            model.ImagePosition = schema.ImagePosition;
            model.SubSections = GetSubSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public ArticlesSliderSubSectionViewModel CreateSliderSubSection(ArticlesSubSectionSliderSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ArticlesSliderSubSectionViewModel>(schema);

            model.Content = schema.Content;
            model.ShowBackground = schema.ShowGreyBackground;
            model.ShowTimelineArrows = schema.ShowTimelineArrows;
            model.SliderItems = GetSliderItemViewModels(schema.SliderItems);
            model.SubSections = GetSubSubSectionViewModels(schema.SubSections, model.HtmlId);
            return model;
        }

        public IEnumerable<ArticlesSubSectionViewModel> GetSubSubSectionViewModels(BlockListModel subSubSections, string parentHtmlId)
        {
            var viewModels = new List<ArticlesSubSectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(subSubSections))
            {
                return viewModels;
            }

            foreach (var section in subSubSections)
            {
                ArticlesSubSectionViewModel viewModel = null;

                switch (section.Content)
                {
                    case ArticlesSubSectionRichTextSchema richText:
                        viewModel = CreateRichTextSubSection(richText, parentHtmlId);
                        break;
                    case ArticlesSubSectionImageGallerySchema imageGallery:
                        viewModel = CreateImageGallerySubSection(imageGallery, parentHtmlId);
                        break;
                    case ArticlesSubSectionImageRichTextSchema imageRichText:
                        viewModel = CreateImageRichTextSubSection(imageRichText, parentHtmlId);
                        break;
                    case ArticlesSubSectionImageCodeSchema imageCode:
                        viewModel = CreateImageCodeSubSection(imageCode, parentHtmlId);
                        break;
                    case ArticlesSubSectionSliderSchema slider:
                        viewModel = CreateSliderSubSection(slider, parentHtmlId);
                        break;
                }

                if (viewModel != null)
                {
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

    }
}
