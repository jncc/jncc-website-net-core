
using Amazon.SQS;
using JNCC.PublicWebsite.Core.Notifications;
using JNCC.PublicWebsite.Core.Options;
using Microsoft.AspNetCore.Rewrite;
using SEOChecker.Core.Notifications;
using Umbraco.Cms.Core.Notifications;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddAWSService<IAmazonSQS>();

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
    .AddAzureBlobMediaFileSystem()
    .AddAzureBlobImageSharpCache()
    .AddNotificationHandler<XmlSitemapGeneratedNotification, SitemapGeneratedNotificationHandler>()
    .AddNotificationHandler<ContentCacheRefresherNotification, ContentCacheRefresherNotificationHandler>()
    .AddNotificationHandler<MediaCacheRefresherNotification, MediaCacheRefresherNotificationHandler>()
    .Build();

builder.Services.Configure<AmazonServiceConfigurationOptions>(builder.Configuration.GetSection(AmazonServiceConfigurationOptions.AmazonServiceConfiguration));

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/500.html");
}

//Cookie hijacking and protocol downgrade attacks Protection (Strict-Transport-Security Header (HSTS)
app.UseHsts(options => options.MaxAge(days: 365));

//X-frame-options
app.UseXfo(options => options.SameOrigin());

//Content/MIME Sniffing Protection
app.UseXContentTypeOptions();

app.UseReferrerPolicy(opts => opts.StrictOrigin());

//Cross-site scripting Protection (X-XSS-Protection header)
app.UseXXssProtection(options => options.EnabledWithBlockMode());

//Use the IIS Rewrite Middleware
app.UseRewriter(new RewriteOptions().AddIISUrlRewrite(builder.Environment.ContentRootFileProvider, "IISUrlRewrite.xml"));

app.UseStatusCodePagesWithReExecute("/UrlDoesntExist");

app.UseHttpsRedirection();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
