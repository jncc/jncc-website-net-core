using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Text;
using Umbraco.Community.CSPManager.Services;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class PageIncludesService : IPageIncludesService
    {
        private readonly ICspService _cspService;

        public PageIncludesService(ICspService cspService)
        {
            _cspService = cspService ?? throw new ArgumentNullException(nameof(cspService));
        }


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

        public string GetHeadIncludes(IGlobalIncludesComposition globalIncludes, IPageSpecificIncludesComposition pageSpecificIncludes, HttpContext context)
        {
            var includesBuilder = new StringBuilder();

            if (globalIncludes != null)
            {

                if (string.IsNullOrWhiteSpace(globalIncludes.GlobalHeadIncludes) == false)
                {
                    includesBuilder.Append(globalIncludes.GlobalHeadIncludes);
                }

                if (pageSpecificIncludes != null &&
                    string.IsNullOrWhiteSpace(pageSpecificIncludes.PageSpecificHeadIncludes) == false)
                {
                    includesBuilder.Append(pageSpecificIncludes.PageSpecificHeadIncludes);
                }
            }

            return AddNonces(includesBuilder.ToString(), context);
        }

        public string GetBodyIncludes(IGlobalIncludesComposition globalIncludes, IPageSpecificIncludesComposition pageSpecificIncludes, HttpContext context)
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


            return AddNonces(includesBuilder.ToString(), context);
        }

        private string AddNonces(string includes, HttpContext context)
        {
            if (includes.Contains("<script", StringComparison.OrdinalIgnoreCase))
            {
                var nonce = _cspService.GetCspScriptNonce(context);

                includes = includes.Replace("<script", $"<script nonce=\"{nonce}\"");
            }

            if (includes.Contains("<style", StringComparison.OrdinalIgnoreCase))
            {
                var nonce = _cspService.GetCspStyleNonce(context);

                includes = includes.Replace("<style", $"<style nonce=\"{nonce}\"");
            }

            return includes;
        }
    }
}
