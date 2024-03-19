using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IArticlesImageGallerySectionViewModel
    {
        IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}