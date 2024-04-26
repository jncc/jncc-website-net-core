using JNCC.PublicWebsite;

var builder = Host.CreateDefaultBuilder(args)
                .ConfigureUmbracoDefaults()
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .ConfigureLogging(x => x.ClearProviders());

var host = builder.Build();
host.Run();
