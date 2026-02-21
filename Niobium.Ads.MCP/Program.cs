using Niobium.Ads.MCP;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddLogging()
    .AddHttpClient()
    .AddTransient<IMetaAdsLibrary, TestAdsLibrary>()
    .AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

WebApplication app = builder.Build();
app.UseMiddleware<ApiKeyMiddleware>();
app.MapControllers();
app.MapMcp();
app.Run();