using JNCC.PublicWebsite.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    public interface ISearchIndexingQueueService
    {
        void QueueUpsert(SearchIndexDocumentModel document);
        void QueueDelete(SearchIndexDocumentModel document);
    }
}
