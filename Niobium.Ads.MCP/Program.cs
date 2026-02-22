using System.Text.Json;
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

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
});

WebApplication app = builder.Build();
app.UseMiddleware<ApiKeyMiddleware>();
app.MapControllers();
app.MapMcp();
app.Run();