using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Blocks;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IScienceDetailsPageService
    {
        IEnumerable<NavigationItemViewModel> GetCategories(ScienceDetailsPage model);
        IEnumerable<ScienceDetailsSectionViewModel> GetSectionViewModels(BlockListModel mainContent);
        IEnumerable<ScienceDetailsSectionViewModel> GetImageTextSectionViewModels(BlockListModel imageTextSection);
        IEnumerable<ScienceDetailsSubSectionViewModel> GetSubSectionViewModels(BlockListModel subSections, string parentHtmlId);
        ScienceDetailsRichTextSectionViewModel CreateRichTextSection(ScienceDetailsSectionRichTextSchema schema);
        ScienceDetailsImageGallerySectionViewModel CreateImageGallerySection(ScienceDetailsSectionImageGallerySchema schema);
        ScienceDetailsRichTextSubSectionViewModel CreateRichTextSubSection(ScienceDetailsSubSectionRichTextSchema schema, string parentSectionHtmlId);
        ScienceDetailsImageGallerySubSectionViewModel CreateImageGallerySubSection(ScienceDetailsSubSectionImageGallerySchema schema, string parentSectionHtmlId);
        IEnumerable<ImageGalleryItemViewModel> CreateSectionImageGallery(IEnumerable<MediaWithCrops> images);
        ScienceDetailsImageRichTextSectionViewModel CreateImageRichTextSection(ScienceDetailsSectionImageTextSchema schema);
        ScienceDetailsImageRichTextSectionViewModel CreateIndividualImageRichTextSection(ScienceDetailsIndividualSectionImageTextSchema schema);
        ScienceDetailsImageRichTextSubSectionViewModel CreateImageRichTextSubSection(ScienceDetailsSubSectionImageRichTextSchema schema, string parentSectionHtmlId);
        ScienceDetailsImageCodeSectionViewModel CreateImageCodeSection(ScienceDetailsSectionImageCodeSchema schema);
        ScienceDetailsImageCodeSectionViewModel CreateIndividualImageCodeSection(ScienceDetailsIndividualSectionImageCodeSchema schema);
        ScienceDetailsImageCodeSubSectionViewModel CreateImageCodeSubSection(ScienceDetailsSubSectionImageCodeSchema schema, string parentSectionHtmlId);
        ScienceDetailsSliderSectionViewModel CreateSliderSection(ScienceDetailsSectionSliderSchema schema);
        IEnumerable<ScienceSliderSchemaViewModel> GetSliderItemViewModels(BlockListModel sliderItems);
        ScienceSliderSchemaViewModel CreateHeadingTextSliderItem(ScienceSliderSchemaHeadingText schema);
        ScienceSliderSchemaViewModel CreateImageTextSliderItem(ScienceSliderSchemaImageText schema);
        ScienceDetailsSliderSubSectionViewModel CreateSliderSubSection(ScienceDetailsSubSectionSliderSchema schema, string parentSectionHtmlId);
        IEnumerable<ScienceDetailsSubSectionViewModel> GetSubSubSectionViewModels(BlockListModel subSubSections, string parentHtmlId);
    }
}
