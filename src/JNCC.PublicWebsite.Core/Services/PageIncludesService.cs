using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Globalization;
using System.Text;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class PageIncludesService : IPageIncludesService
    {

        public PageAttributesViewModel GetPageAttributesViewModel(IPageSpecificIncludesComposition pageSpecificIncludesComposition)
        {
            var ltrValue = HtmlTextDirectionalities.Auto;

            if (CultureInfo.CurrentCulture != null && CultureInfo.CurrentCulture.TextInfo != null)
            {
                ltrValue = CultureInfo.CurrentCulture.TextInfo.IsRightToLeft ? HtmlTextDirectionalities.RightToLeft : HtmlTextDirectionalities.LeftToRight;
            }

            var viewmodel = new PageAttributesViewModel()
            {
                HTMLLangRef = pageSpecificIncludesComposition.HTmllangRef,
                LTRValue = ltrValue,
            };
            return viewmodel;
        }

        public string GetHeadIncludes(IGlobalIncludesComposition globalIncludes, IPageSpecificIncludesComposition pageSpecificIncludes)
        {
            var includesBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(globalIncludes.GlobalHeadIncludes) == false)
            {
                includesBuilder.Append(globalIncludes.GlobalHeadIncludes);
            }

            if (pageSpecificIncludes != null &&
                string.IsNullOrWhiteSpace(pageSpecificIncludes.PageSpecificHeadIncludes) == false)
            {
                includesBuilder.Append(pageSpecificIncludes.PageSpecificHeadIncludes);
            }

            return includesBuilder.ToString();
        }

        public string GetBodyIncludes(IGlobalIncludesComposition globalIncludes, IPageSpecificIncludesComposition pageSpecificIncludes)
        {
            var includesBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(globalIncludes.GlobalBodyIncludes) == false)
            {
                includesBuilder.Append(globalIncludes.GlobalBodyIncludes);
            }

            if (pageSpecificIncludes != null &&
                string.IsNullOrWhiteSpace(pageSpecificIncludes.PageSpecificBodyIncludes) == false)
            {
                includesBuilder.Append(pageSpecificIncludes.PageSpecificBodyIncludes);
            }


            return includesBuilder.ToString();
        }
    }
}
