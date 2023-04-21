using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNCC.PublicWebsite.Core.Models.Custom
{
    /// <summary>
    /// The main model that's returned by the JNCC resource API
    /// </summary>
    public class ResourceModel
    {
        [JsonProperty("metadata")]
        public ResourceMetaData MetaData { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("data")]
        public List<ResourceData> Data { get; set; }

        [JsonProperty("timestamp_utc")]
        public DateTime Timestamp { get; set; }

        public string DigitalObjectIdentifier { get; set; }
        public string Citation { get; set; }

        public ResourceImage Image { get; set; }
    }

    /// <summary>
    /// The model for resource metadata
    /// </summary>
    public class ResourceMetaData
    {
        [JsonProperty("lineage")]
        public string Lineage { get; set; }

        [JsonProperty("boundingBox")]
        public ResourceBounding BoundingBox { get; set; }

        [JsonProperty("keywords")]
        public List<ResourceKeyword> Keywords { get; set; }

        [JsonProperty("limitationsOnPublicAccess")]
        public string LimitationsOnPublicAccess { get; set; }

        [JsonProperty("dataFormat")]
        public string DataFormat { get; set; }

        [JsonProperty("topicCategory")]
        public string TopicCategory { get; set; }

        [JsonProperty("abstract")]
        public string Abstract { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("spatialReferenceSystem")]
        public string SpatialReferenceSystem { get; set; }

        [JsonProperty("metadataDate")]
        public string MetaDataDate { get; set; }

        [JsonProperty("datasetReferenceDate")]
        public string DatasetReferenceDate { get; set; }

        [JsonProperty("metadataPointOfContact")]
        public ResourceContact MetadataPointOfContact { get; set; }

        [JsonProperty("metadataLanguage")]
        public string MetadataLanguage { get; set; }

        [JsonProperty("temporalExtent")]
        public ResourceExtent TemporalExtent { get; set; }

        [JsonProperty("useConstraints")]
        public string UseConstraints { get; set; }

        [JsonProperty("responsibleOrganisation")]
        public ResourceContact ResponsibleOrganisation { get; set; }

        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        public string AdditionalInformationSource { get; set; }

        public string Copyright { get; set; }
    }

    /// <summary>
    /// The model for a resource extent
    /// </summary>
    public class ResourceExtent
    {
        [JsonProperty("begin")]
        public string Begin { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }
    }

    /// <summary>
    /// The model for a resource contact
    /// </summary>
    public class ResourceContact
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }

    /// <summary>
    /// The model for a resource keyword
    /// </summary>
    public class ResourceKeyword
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("vocab")]
        public string Vocab { get; set; }
    }

    /// <summary>
    /// The model for a resource bounds
    /// </summary>
    public class ResourceBounding
    {
        [JsonProperty("east")]
        public decimal East { get; set; }

        [JsonProperty("west")]
        public decimal West { get; set; }

        [JsonProperty("north")]
        public decimal North { get; set; }

        [JsonProperty("south")]
        public decimal South { get; set; }
    }

    /// <summary>
    /// The model for resource link data
    /// </summary>
    public class ResourceData
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("http")]
        public ResourceHttp Http { get; set; }
    }

    /// <summary>
    /// The model for the details of link files
    /// </summary>
    public class ResourceHttp
    {
        [JsonProperty("fileBytes")]
        public long FileBytes { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("fileExtension")]
        public string FileExtension { get; set; }

        [JsonProperty("fileBase64")]
        public string FileBase64 { get; set; }
    }

    /// <summary>
    /// The model for resource images
    /// </summary>
    public class ResourceImage
    {
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public ResourceImageCrops Crops { get; set; }
    }

    /// <summary>
    /// The model for resource image crops
    /// </summary>
    public class ResourceImageCrops
    {
        public string SquareUrl { get; set; }
        public string ThumbnailUrl { get; set; }
    }

    /// <summary>
    /// The model for the sitemap API endpoint
    /// </summary>
    public class ResourceSitemap
    {
        public string Id { get; set; }

        public DateTime LastUpdated { get; set; }
    }

}
