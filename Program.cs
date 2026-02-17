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
    .WithToolsFromAssembly()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

WebApplication app = builder.Build();
app.MapMcp();
await app.RunAsync();