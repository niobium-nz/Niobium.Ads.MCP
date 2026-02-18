using Niobium.Ads.MCP;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddLogging()
    .AddHttpClient()
    .AddTransient<IMetaAdsLibrary, ScrapecreatorsMetaAdsLibrary>()
    .AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

WebApplication app = builder.Build();
app.UseMiddleware<ApiKeyMiddleware>();
app.MapMcp();
app.Run();