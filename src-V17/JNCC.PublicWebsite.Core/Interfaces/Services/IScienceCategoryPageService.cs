using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Blocks;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IScienceCategoryPageService
    {
        IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> GetCategorisedPages(ScienceCategoryPage scienceCategoryPage);
        IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> GetRelatedCategories(ScienceCategoryPage scienceCategoryPage);
        IEnumerable<ScienceCategorySectionViewModel> GetSectionViewModels(BlockListModel mainContent);
        IEnumerable<ScienceCategorySectionViewModel> GetImageTextSectionViewModels(BlockListModel imageTextSection);
        IEnumerable<ScienceCategorySubSectionViewModel> GetSubSectionViewModels(BlockListModel subSections, string parentHtmlId);
        ScienceCategoryRichTextSectionViewModel CreateRichTextSection(ScienceCategorySectionRichTextSchema schema);
        ScienceCategoryImageGallerySectionViewModel CreateImageGallerySection(ScienceCategorySectionImageGallerySchema schema);
        ScienceCategoryRichTextSubSectionViewModel CreateRichTextSubSection(ScienceCategorySubSectionRichTextSchema schema, string parentSectionHtmlId);
        ScienceCategoryImageGallerySubSectionViewModel CreateImageGallerySubSection(ScienceCategorySubSectionImageGallerySchema schema, string parentSectionHtmlId);
        IEnumerable<ImageGalleryItemViewModel> CreateSectionImageGallery(IEnumerable<MediaWithCrops> images);
        ScienceCategoryImageRichTextSectionViewModel CreateImageRichTextSection(ScienceCategorySectionImageTextSchema schema);
        ScienceCategoryImageRichTextSectionViewModel CreateIndividualImageRichTextSection(ScienceCategoryIndividualSectionImageTextSchema schema);
        ScienceCategoryImageRichTextSubSectionViewModel CreateImageRichTextSubSection(ScienceCategorySubSectionImageRichTextSchema schema, string parentSectionHtmlId);
        ScienceCategoryImageCodeSectionViewModel CreateImageCodeSection(ScienceCategorySectionImageCodeSchema schema);
        ScienceCategoryImageCodeSectionViewModel CreateIndividualImageCodeSection(ScienceCategoryIndividualSectionImageCodeSchema schema);
        ScienceCategoryImageCodeSubSectionViewModel CreateImageCodeSubSection(ScienceCategorySubSectionImageCodeSchema schema, string parentSectionHtmlId);
        ScienceCategorySliderSectionViewModel CreateSliderSection(ScienceCategorySectionSliderSchema schema);
        IEnumerable<ScienceSliderSchemaViewModel> GetSliderItemViewModels(BlockListModel sliderItems);
        ScienceSliderSchemaViewModel CreateHeadingTextSliderItem(ScienceSliderSchemaHeadingText schema);
        ScienceSliderSchemaViewModel CreateImageTextSliderItem(ScienceSliderSchemaImageText schema);
        ScienceCategorySliderSubSectionViewModel CreateSliderSubSection(ScienceCategorySubSectionSliderSchema schema, string parentSectionHtmlId);
    }
}
