using AdsTransparency;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddLogging()
    .AddHttpClient()
    .AddTransient<IMetaAdsLibrary, ScrapecreatorsMetaAdsLibrary>()
    .AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

WebApplication app = builder.Build();
app.MapMcp();
app.Run();