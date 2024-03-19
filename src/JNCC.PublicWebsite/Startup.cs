using Amazon.SQS;
using JNCC.PublicWebsite.Core.Notifications;
using JNCC.PublicWebsite.Core.Options;
using Microsoft.AspNetCore.Rewrite;
using SEOChecker.Core.Notifications;
using Umbraco.Cms.Core.Notifications;

namespace JNCC.PublicWebsite
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="webHostEnvironment">The web hosting environment.</param>
        /// <param name="config">The configuration.</param>
        /// <remarks>
        /// Only a few services are possible to be injected here https://github.com/dotnet/aspnetcore/issues/9337.
        /// </remarks>
        public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _env = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <remarks>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940.
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAWSService<IAmazonSQS>();

            services.AddUmbraco(_env, _config)
                .AddBackOffice()
                .AddWebsite()
                .AddComposers()
                .AddNotificationHandler<ContentPublishedNotification, ContentPublishedNotificationHandler>()
                .AddNotificationHandler<ContentUnpublishedNotification, ContentUnpublishedPublishedNotificationHandler>()
                .AddNotificationHandler<ContentDeletedNotification, ContentDeletedNotificationHandler>()
                .AddNotificationHandler<MediaDeletedNotification, MediaDeletedNotificationHandler>()
                .AddNotificationHandler<MediaSavedNotification, MediaSavedNotificationHandler>()
                .AddNotificationHandler<XmlSitemapGeneratedNotification, SitemapGeneratedNotificationHandler>()
                .Build();

            services.Configure<AmazonServiceConfigurationOptions>(_config.GetSection(AmazonServiceConfigurationOptions.AmazonServiceConfiguration));
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The web hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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

            //Cross-site scripting Protection (X-XSS-Protection header)
            app.UseXXssProtection(options => options.EnabledWithBlockMode());

            //Use the IIS Rewrite Middleware
            app.UseRewriter(new RewriteOptions().AddIISUrlRewrite(env.ContentRootFileProvider, "IISUrlRewrite.xml"));

            app.UseUmbraco()
                .WithMiddleware(u =>
                {
                    u.UseBackOffice();
                    u.UseWebsite();
                })
                .WithEndpoints(u =>
                {
                    u.UseInstallerEndpoints();
                    u.UseBackOfficeEndpoints();
                    u.UseWebsiteEndpoints();
                });
        }
    }
}
