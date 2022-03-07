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
    internal sealed class ScienceDetailsPageService : IScienceDetailsPageService
    {
        private readonly INavigationItemService _navigationItemService;
        private readonly IShortStringHelper _shortStringHelper;
        private readonly ISciencePageCategoriesProvider _sciencePageCategoriesProvider;
        public ScienceDetailsPageService(ISciencePageCategoriesProvider sciencePageCategoriesProvider, INavigationItemService navigationItemService, IShortStringHelper shortStringHelper)
        {
            _sciencePageCategoriesProvider = sciencePageCategoriesProvider;
            _navigationItemService = navigationItemService ?? throw new ArgumentNullException(nameof(navigationItemService));
            _shortStringHelper = shortStringHelper ?? throw new ArgumentNullException(nameof(shortStringHelper));
        }

        public IEnumerable<NavigationItemViewModel> GetCategories(ScienceDetailsPage model)
        {
            var categories = _sciencePageCategoriesProvider.GetCategories(model);

            if (ExistenceUtility.IsNullOrEmpty(categories))
            {
                return Enumerable.Empty<NavigationItemViewModel>();
            }

            return _navigationItemService.GetViewModels(categories);
        }

        private DateTime? GetReviewedDate(DateTime reviewDate)
        {
            if (reviewDate == default(DateTime))
            {
                return null;
            }

            return reviewDate;
        }

        public IEnumerable<ScienceDetailsSectionViewModel> GetSectionViewModels(BlockListModel mainContent)
        {
            var viewModels = new List<ScienceDetailsSectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(mainContent))
            {
                return viewModels;
            }
            foreach (var section in mainContent)
            {
                ScienceDetailsSectionViewModel viewModel = null;

                switch (section.Content)
                {
                    case ScienceDetailsSectionRichTextSchema richText:
                        viewModel = CreateRichTextSection(richText);
                        break;
                    case ScienceDetailsSectionImageGallerySchema imageGallery:
                        viewModel = CreateImageGallerySection(imageGallery);
                        break;
                    case ScienceDetailsSectionImageTextSchema imageRichText:
                        viewModel = CreateImageRichTextSection(imageRichText);
                        break;
                    case ScienceDetailsSectionImageCodeSchema imageCode:
                        viewModel = CreateImageCodeSection(imageCode);
                        break;
                    case ScienceDetailsSectionSliderSchema slider:
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
        public IEnumerable<ScienceDetailsSectionViewModel> GetImageTextSectionViewModels(BlockListModel imageTextSection)
        {
            var viewModels = new List<ScienceDetailsSectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(imageTextSection))
            {
                return viewModels;
            }

            foreach (var section in imageTextSection)
            {
                ScienceDetailsSectionViewModel viewModel = null;

                switch (section.Content)
                {
                    case ScienceDetailsIndividualSectionImageTextSchema imageRichText:
                        viewModel = CreateIndividualImageRichTextSection(imageRichText);
                        break;
                    case ScienceDetailsIndividualSectionImageCodeSchema imageCode:
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

        public IEnumerable<ScienceDetailsSubSectionViewModel> GetSubSectionViewModels(BlockListModel subSections, string parentHtmlId)
        {
            var viewModels = new List<ScienceDetailsSubSectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(subSections))
            {
                return viewModels;
            }

            foreach (var section in subSections)
            {
                ScienceDetailsSubSectionViewModel viewModel = null;

                switch (section.Content)
                {
                    case ScienceDetailsSubSectionRichTextSchema richText:
                        viewModel = CreateRichTextSubSection(richText, parentHtmlId);
                        break;
                    case ScienceDetailsSubSectionImageGallerySchema imageGallery:
                        viewModel = CreateImageGallerySubSection(imageGallery, parentHtmlId);
                        break;
                    case ScienceDetailsSubSectionImageRichTextSchema imageRichText:
                        viewModel = CreateImageRichTextSubSection(imageRichText, parentHtmlId);
                        break;
                    case ScienceDetailsSubSectionImageCodeSchema imageCode:
                        viewModel = CreateImageCodeSubSection(imageCode, parentHtmlId);
                        break;
                    case ScienceDetailsSubSectionSliderSchema slider:
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

        public IEnumerable<ScienceDetailsSubSectionViewModel> GetSubSubSectionViewModels(BlockListModel subSubSections, string parentHtmlId)
        {
            var viewModels = new List<ScienceDetailsSubSectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(subSubSections))
            {
                return viewModels;
            }

            foreach (var section in subSubSections)
            {
                ScienceDetailsSubSectionViewModel viewModel = null;

                switch (section.Content)
                {
                    case ScienceDetailsSubSectionRichTextSchema richText:
                        viewModel = CreateRichTextSubSection(richText, parentHtmlId);
                        break;
                    case ScienceDetailsSubSectionImageGallerySchema imageGallery:
                        viewModel = CreateImageGallerySubSection(imageGallery, parentHtmlId);
                        break;
                    case ScienceDetailsSubSectionImageRichTextSchema imageRichText:
                        viewModel = CreateImageRichTextSubSection(imageRichText, parentHtmlId);
                        break;
                    case ScienceDetailsSubSectionImageCodeSchema imageCode:
                        viewModel = CreateImageCodeSubSection(imageCode, parentHtmlId);
                        break;
                    case ScienceDetailsSubSectionSliderSchema slider:
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

        private TViewModel CreateSection<TViewModel>(ScienceDetailsSectionBaseSchema schema, string parentSectionHtmlId = null) where TViewModel : ScienceDetailsSectionViewModelBase, new()
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

        private string GetPartialViewName(ScienceDetailsSectionBaseSchema schema)
        {
            switch (schema.ContentType.Alias)
            {
                case ScienceDetailsSectionRichTextSchema.ModelTypeAlias:
                case ScienceDetailsSubSectionRichTextSchema.ModelTypeAlias:
                    return ScienceDetailsPartialViewNames.RichText;
                case ScienceDetailsSubSectionImageGallerySchema.ModelTypeAlias:
                case ScienceDetailsSectionImageGallerySchema.ModelTypeAlias:
                    return ScienceDetailsPartialViewNames.ImageGallery;
                case ScienceDetailsIndividualSectionImageTextSchema.ModelTypeAlias:
                case ScienceDetailsSectionImageTextSchema.ModelTypeAlias:
                case ScienceDetailsSubSectionImageRichTextSchema.ModelTypeAlias:
                    return ScienceDetailsPartialViewNames.ImageRichText;
                case ScienceDetailsIndividualSectionImageCodeSchema.ModelTypeAlias:
                case ScienceDetailsSectionImageCodeSchema.ModelTypeAlias:
                case ScienceDetailsSubSectionImageCodeSchema.ModelTypeAlias:
                    return ScienceDetailsPartialViewNames.ImageCode;
                case ScienceDetailsSectionSliderSchema.ModelTypeAlias:
                case ScienceDetailsSubSectionSliderSchema.ModelTypeAlias:
                    return ScienceDetailsPartialViewNames.Slider;
                default:
                    throw new NotSupportedException($"Document Type, {schema.ContentType.Alias}, is not currently supported.");
            }
        }

        public ScienceDetailsRichTextSectionViewModel CreateRichTextSection(ScienceDetailsSectionRichTextSchema schema)
        {
            var model = CreateSection<ScienceDetailsRichTextSectionViewModel>(schema);

            model.Content = schema.Content;
            
            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public ScienceDetailsImageGallerySectionViewModel CreateImageGallerySection(ScienceDetailsSectionImageGallerySchema schema)
        {
            var model = CreateSection<ScienceDetailsImageGallerySectionViewModel>(schema);

            model.Images = CreateSectionImageGallery(schema.Images);

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public ScienceDetailsRichTextSubSectionViewModel CreateRichTextSubSection(ScienceDetailsSubSectionRichTextSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceDetailsRichTextSubSectionViewModel>(schema, parentSectionHtmlId);
            model.Content = schema.Content;

            model.SubSections = GetSubSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public ScienceDetailsImageGallerySubSectionViewModel CreateImageGallerySubSection(ScienceDetailsSubSectionImageGallerySchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceDetailsImageGallerySubSectionViewModel>(schema, parentSectionHtmlId);

            model.Images = CreateSectionImageGallery(schema.Images);
            model.SubSections = GetSubSubSectionViewModels(schema.SubSections, model.HtmlId);

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
                if(galleryItem?.Content is Image image)
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

        public ScienceDetailsImageRichTextSectionViewModel CreateImageRichTextSection(ScienceDetailsSectionImageTextSchema schema)
        {
            var model = CreateSection<ScienceDetailsImageRichTextSectionViewModel>(schema);

            model.Content = schema.Content;
            if (schema.Image != null)
            {
                if(schema.Image?.Content is Image image)
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
        public ScienceDetailsImageRichTextSectionViewModel CreateIndividualImageRichTextSection(ScienceDetailsIndividualSectionImageTextSchema schema)
        {
            var model = CreateSection<ScienceDetailsImageRichTextSectionViewModel>(schema);

            model.Content = schema.Content;

            if (schema.Image != null)
            {
                if (schema.Image is Image image)
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


            model.ImagePosition = schema.Value<string>("imagePosition");

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public ScienceDetailsImageRichTextSubSectionViewModel CreateImageRichTextSubSection(ScienceDetailsSubSectionImageRichTextSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceDetailsImageRichTextSubSectionViewModel>(schema, parentSectionHtmlId);

            model.Content = schema.Content;
            if (schema.Image != null)
            {
                if(schema.Image?.Content is Image image)
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

        public ScienceDetailsImageCodeSectionViewModel CreateImageCodeSection(ScienceDetailsSectionImageCodeSchema schema)
        {
            var model = CreateSection<ScienceDetailsImageCodeSectionViewModel>(schema);

            model.Content = schema.Content;
            model.ImageCode = schema.ImageCode;
            model.ImagePosition = schema.ImagePosition;
            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }
        public ScienceDetailsImageCodeSectionViewModel CreateIndividualImageCodeSection(ScienceDetailsIndividualSectionImageCodeSchema schema)
        {
            var model = CreateSection<ScienceDetailsImageCodeSectionViewModel>(schema);

            model.Content = schema.Content;
            model.ImageCode = schema.ImageCode;
            model.ImagePosition = schema.ImagePosition;
            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public ScienceDetailsImageCodeSubSectionViewModel CreateImageCodeSubSection(ScienceDetailsSubSectionImageCodeSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceDetailsImageCodeSubSectionViewModel>(schema, parentSectionHtmlId);

            model.Content = schema.Content;
            model.ImageCode = schema.ImageCode;
            model.ImagePosition = schema.ImagePosition;
            model.SubSections = GetSubSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        public ScienceDetailsSliderSectionViewModel CreateSliderSection(ScienceDetailsSectionSliderSchema schema)
        {
            var model = CreateSection<ScienceDetailsSliderSectionViewModel>(schema);

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

        public ScienceDetailsSliderSubSectionViewModel CreateSliderSubSection(ScienceDetailsSubSectionSliderSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceDetailsSliderSubSectionViewModel>(schema);

            model.Content = schema.Content;
            model.ShowBackground = schema.ShowGreyBackground;
            model.ShowTimelineArrows = schema.ShowTimelineArrows;
            model.SliderItems = GetSliderItemViewModels(schema.SliderItems);
            model.SubSections = GetSubSubSectionViewModels(schema.SubSections, model.HtmlId);
            return model;
        }
    }
}
