using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public class AccordionItemViewModel : IAccordionHeaderViewModel
    {
        public AccordionItemViewModel()
        {
        }

        public AccordionItemViewModel(string htmlId, string title, IHtmlEncodedString content) : this()
        {
            HtmlId = htmlId;
            Title = title;
            Content = content;
        }

        public string HtmlId { get; set; }
        public string Title { get; set; }
        public IHtmlEncodedString Content { get; set; }
    }

    public interface IAccordionHeaderViewModel
    {
        string HtmlId { get; set; }
        string Title { get; set; }
    }
}