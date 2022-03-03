using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Interfaces.Providers;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Strings;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceCategoryPageService : IScienceCategoryPageService
    {
        private readonly INavigationItemService _navigationItemService;
        private readonly IShortStringHelper _shortStringHelper;
        private readonly IScienceDetailsPageProvider _pagesProvider;

        public ScienceCategoryPageService(IScienceDetailsPageProvider pagesProvider, INavigationItemService navigationItemService, IShortStringHelper shortStringHelper)
        {
            _navigationItemService = navigationItemService ?? throw new ArgumentNullException(nameof(navigationItemService));
            _shortStringHelper = shortStringHelper ?? throw new ArgumentNullException(nameof(shortStringHelper));
            _pagesProvider = pagesProvider ?? throw new ArgumentNullException(nameof(pagesProvider));
        }

        public IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> GetCategorisedPages(ScienceCategoryPage scienceCategoryPage)
        {
            var pages = _pagesProvider.GetByCategory(scienceCategoryPage);

            if (ExistenceUtility.IsNullOrEmpty(pages))
            {
                return null;
            }

            return pages.CategorisePages();
        }

        public IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> GetRelatedCategories(ScienceCategoryPage scienceCategoryPage)
        {
            if (ExistenceUtility.IsNullOrEmpty(scienceCategoryPage.RelatedCategories))
            {
                return null;
            }

            if(scienceCategoryPage.RelatedCategories is IEnumerable<IScienceCategorisablePage> relatedCategories)
            {
                return relatedCategories.CategorisePages();
            }

            return null;
        }

        public IEnumerable<ScienceCategorySectionViewModel> GetImageTextSectionViewModels(BlockListModel imageTextSection)
        {
            var viewModels = new List<ScienceCategorySectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(imageTextSection))
            {
                return viewModels;
            }

            foreach (var section in imageTextSection)
            {
                ScienceCategorySectionViewModel viewModel = null;

                switch (section.Content)
                {
                    case ScienceCategoryIndividualSectionImageTextSchema imageRichText:
                        viewModel = CreateIndividualImageRichTextSection(imageRichText);
                        break;
                    case ScienceCategoryIndividualSectionImageCodeSchema imageCode:
                        viewModel = CreateIndividualImageCodeSection(imageCode);
                        break;
                }

                if (viewModel != null)
                {
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        public IEnumerable<ScienceCategorySectionViewModel> GetSectionViewModels(BlockListModel mainContent)
        {
            var viewModels = new List<ScienceCategorySectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(mainContent))
            {
                return viewModels;
            }

            foreach (var section in mainContent)
            {
                ScienceCategorySectionViewModel viewModel = null;

                switch (section.Content)
                {
                    case ScienceCategorySectionRichTextSchema richText:
                        viewModel = CreateRichTextSection(richText);
                        break;
                    case ScienceCategorySectionImageGallerySchema imageGallery:
                        viewModel = CreateImageGallerySection(imageGallery);
                        break;
                    case ScienceCategorySectionImageTextSchema imageRichText:
                        viewModel = CreateImageRichTextSection(imageRichText);
                        break;
                    case ScienceCategorySectionImageCodeSchema imageCode:
                        viewModel = CreateImageCodeSection(imageCode);
                        break;
                    case ScienceCategorySectionSliderSchema slider:
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

        public IEnumerable<ScienceCategorySubSectionViewModel> GetSubSectionViewModels(BlockListModel subSections, string parentHtmlId)
        {
            var viewModels = new List<ScienceCategorySubSectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(subSections))
            {
                return viewModels;
            }

            foreach (var section in subSections)
            {
                ScienceCategorySubSectionViewModel viewModel = null;

                switch (section.Content)
                {
                    case ScienceCategorySubSectionRichTextSchema richText:
                        viewModel = CreateRichTextSubSection(richText, parentHtmlId);
                        break;
                    case ScienceCategorySubSectionImageGallerySchema imageGallery:
                        viewModel = CreateImageGallerySubSection(imageGallery, parentHtmlId);
                        break;
                    case ScienceCategorySubSectionImageRichTextSchema imageRichText:
                        viewModel = CreateImageRichTextSubSection(imageRichText, parentHtmlId);
                        break;
                    case ScienceCategorySubSectionImageCodeSchema imageCode:
                        viewModel = CreateImageCodeSubSection(imageCode, parentHtmlId);
                        break;
                    case ScienceCategorySubSectionSliderSchema slider:
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

        public TViewModel CreateSection<TViewModel>(ScienceCategorySectionBaseSchema schema, string parentSectionHtmlId = null) where TViewModel : ScienceCategorySectionViewModelBase, new()
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

        public string GetPartialViewName(ScienceCategorySectionBaseSchema schema)
        {
            switch (schema.ContentType.Alias)
            {

                case ScienceCategorySectionRichTextSchema.ModelTypeAlias:
                case ScienceCategorySubSectionRichTextSchema.ModelTypeAlias:
                    return ScienceCategoryPartialViewNames.RichText;
                case ScienceCategorySubSectionImageGallerySchema.ModelTypeAlias:
                case ScienceCategorySectionImageGallerySchema.ModelTypeAlias:
                    return ScienceCategoryPartialViewNames.ImageGallery;
                case ScienceCategoryIndividualSectionImageTextSchema.ModelTypeAlias:
                case ScienceCategorySectionImageTextSchema.ModelTypeAlias:
                case ScienceCategorySubSectionImageRichTextSchema.ModelTypeAlias:
                    return ScienceCategoryPartialViewNames.ImageRichText;
                case ScienceCategoryIndividualSectionImageCodeSchema.ModelTypeAlias:
                case ScienceCategorySectionImageCodeSchema.ModelTypeAlias:
                case ScienceCategorySubSectionImageCodeSchema.ModelTypeAlias:
                    return ScienceCategoryPartialViewNames.ImageCode;
                case ScienceCategorySectionSliderSchema.ModelTypeAlias:
                case ScienceCategorySubSectionSliderSchema.ModelTypeAlias:
                    return ScienceCategoryPartialViewNames.Slider;
                default:
                    throw new NotSupportedException($"Document Type, {schema.ContentType.Alias}, is not currently supported.");
            }
        }

        public ScienceCategoryRichTextSectionViewModel CreateRichTextSection(ScienceCategorySectionRichTextSchema schema)
        {
            var model = CreateSection<ScienceCategoryRichTextSectionViewModel>(schema);

            model.Content = schema.Content;

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public ScienceCategoryImageGallerySectionViewModel CreateImageGallerySection(ScienceCategorySectionImageGallerySchema schema)
        {
            var model = CreateSection<ScienceCategoryImageGallerySectionViewModel>(schema);

            model.Images = CreateSectionImageGallery(schema.Images);

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public ScienceCategoryRichTextSubSectionViewModel CreateRichTextSubSection(ScienceCategorySubSectionRichTextSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceCategoryRichTextSubSectionViewModel>(schema, parentSectionHtmlId);

            model.Content = schema.Content;

            return model;
        }

        public ScienceCategoryImageGallerySubSectionViewModel CreateImageGallerySubSection(ScienceCategorySubSectionImageGallerySchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceCategoryImageGallerySubSectionViewModel>(schema, parentSectionHtmlId);

            model.Images = CreateSectionImageGallery(schema.Images);

            return model;
        }

        public IEnumerable<ImageGalleryItemViewModel> CreateSectionImageGallery(IEnumerable<MediaWithCrops> images)
        {
            var viewModels = new List<ImageGalleryItemViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(images))
            {
                return viewModels;
            }

            foreach (var image in images)
            {
                if(image?.Content is Image galleryImage)
                {
                    var viewModel = new ImageGalleryItemViewModel()
                    {
                        Url = galleryImage.Url(),
                        ThumbnailImageUrl = galleryImage.GetCropUrl(cropAlias: ImageCropAliases.Square, urlMode: UrlMode.Absolute),
                        AlternativeText = galleryImage.AltText.IsNullOrWhiteSpace() ? image.Name : galleryImage.AltText,
                        TitleText = galleryImage.TitleText
                    };

                    if (string.IsNullOrEmpty(image.Url()) == false)
                    {
                        viewModels.Add(viewModel);
                    }
                }
            }

            return viewModels;
        }

        public ScienceCategoryImageRichTextSectionViewModel CreateImageRichTextSection(ScienceCategorySectionImageTextSchema schema)
        {
            var model = CreateSection<ScienceCategoryImageRichTextSectionViewModel>(schema);

            model.Content = schema.Content;
            if (schema.Image != null)
            {
                if (schema.Image?.Content is Image image)
                {
                    model.Image = new ImageViewModel()
                    {
                        Url = image.Url(),
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
            model.ImagePosition = schema.GetProperty("imagePosition").GetValue().ToString();

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public ScienceCategoryImageCodeSectionViewModel CreateImageCodeSection(ScienceCategorySectionImageCodeSchema schema)
        {
            var model = CreateSection<ScienceCategoryImageCodeSectionViewModel>(schema);

            model.Content = schema.Content;

            model.ImageCode = schema.ImageCode;

            model.ImagePosition = schema.GetProperty("imagePosition").GetValue().ToString();

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public ScienceCategorySliderSectionViewModel CreateSliderSection(ScienceCategorySectionSliderSchema schema)
        {
            var model = CreateSection<ScienceCategorySliderSectionViewModel>(schema);

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
                if(schema.Image?.Content is Image image)
                {
                    model.Image = new ImageViewModel()
                    {
                        Url = image.Url(),
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

        public ScienceCategoryImageRichTextSectionViewModel CreateIndividualImageRichTextSection(ScienceCategoryIndividualSectionImageTextSchema schema)
        {
            var model = CreateSection<ScienceCategoryImageRichTextSectionViewModel>(schema);

            model.Content = schema.Content;

            if (schema.Image != null)
            {
                if(schema.Image is Image image)
                {
                    model.Image = new ImageViewModel()
                    {
                        Url = image.Url(),
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

        public ScienceCategoryImageCodeSectionViewModel CreateIndividualImageCodeSection(ScienceCategoryIndividualSectionImageCodeSchema schema)
        {
            var model = CreateSection<ScienceCategoryImageCodeSectionViewModel>(schema);

            model.Content = schema.Content;

            model.ImageCode = schema.ImageCode;

            model.ImagePosition = schema.ImagePosition;

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public ScienceCategoryImageRichTextSubSectionViewModel CreateImageRichTextSubSection(ScienceCategorySubSectionImageRichTextSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceCategoryImageRichTextSubSectionViewModel>(schema, parentSectionHtmlId);

            model.Content = schema.Content;
            if (schema.Image != null)
            {
                if(schema.Image?.Content is Image image)
                {
                    model.Image = new ImageViewModel()
                    {
                        Url = image.Url(),
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

        public ScienceCategoryImageCodeSubSectionViewModel CreateImageCodeSubSection(ScienceCategorySubSectionImageCodeSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceCategoryImageCodeSubSectionViewModel>(schema, parentSectionHtmlId);

            model.Content = schema.Content;

            model.ImageCode = schema.ImageCode;

            model.ImagePosition = schema.ImagePosition;

            return model;
        }
        public ScienceCategorySliderSubSectionViewModel CreateSliderSubSection(ScienceCategorySubSectionSliderSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceCategorySliderSubSectionViewModel>(schema);

            model.Content = schema.Content;
            model.ShowBackground = schema.ShowGreyBackground;
            model.ShowTimelineArrows = schema.ShowTimelineArrows;
            model.SliderItems = GetSliderItemViewModels(schema.SliderItems);

            return model;
        }
    }
}
