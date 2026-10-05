using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Blocks;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IArticlesPageService
    {
        IEnumerable<ArticlesSectionViewModel> GetSectionViewModels(BlockListModel mainContent);
        ArticlesRichTextSectionViewModel CreateRichTextSection(ArticlesSectionRichTextSchema schema);
        IEnumerable<ArticlesSubSectionViewModel> GetSubSectionViewModels(BlockListModel subSections, string parentHtmlId);
        ArticlesImageGallerySectionViewModel CreateImageGallerySection(ArticlesSectionImageGallerySchema schema);
        IEnumerable<ImageGalleryItemViewModel> CreateSectionImageGallery(IEnumerable<MediaWithCrops> images);
        ArticlesImageRichTextSectionViewModel CreateImageRichTextSection(ArticlesSectionImageTextSchema schema);
        ArticlesImageCodeSectionViewModel CreateImageCodeSection(ArticlesSectionImageCodeSchema schema);
        ArticlesSliderSectionViewModel CreateSliderSection(ArticlesSectionSliderSchema schema);
        IEnumerable<ScienceSliderSchemaViewModel> GetSliderItemViewModels(BlockListModel sliderItems);
        ScienceSliderSchemaViewModel CreateHeadingTextSliderItem(ScienceSliderSchemaHeadingText schema);
        ScienceSliderSchemaViewModel CreateImageTextSliderItem(ScienceSliderSchemaImageText schema);

        //sub sections
        ArticlesRichTextSubSectionViewModel CreateRichTextSubSection(ArticlesSubSectionRichTextSchema schema, string parentSectionHtmlId);
        ArticlesImageGallerySubSectionViewModel CreateImageGallerySubSection(ArticlesSubSectionImageGallerySchema schema, string parentSectionHtmlId);
        ArticlesImageRichTextSubSectionViewModel CreateImageRichTextSubSection(ArticlesSubSectionImageRichTextSchema schema, string parentSectionHtmlId);
        ArticlesImageCodeSubSectionViewModel CreateImageCodeSubSection(ArticlesSubSectionImageCodeSchema schema, string parentSectionHtmlId);
        ArticlesSliderSubSectionViewModel CreateSliderSubSection(ArticlesSubSectionSliderSchema schema, string parentSectionHtmlId);
        IEnumerable<ArticlesSubSectionViewModel> GetSubSubSectionViewModels(BlockListModel subSubSections, string parentHtmlId);

    }
}
