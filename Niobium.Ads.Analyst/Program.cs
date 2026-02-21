using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Niobium.Ads.Analyst;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddTransient<KeywordsPlanner>()
    .AddTransient<AdsDiscoverer>()
    .AddTransient<VendorProfiler>()
    .AddTransient<AnalystWorkflow>()
    .AddSingleton(sp => new AIProjectClient(
        new Uri("https://whaneus2.services.ai.azure.com/api/projects/firstProject"),
        new DefaultAzureCredential(),
        new AIProjectClientOptions { NetworkTimeout = TimeSpan.FromMinutes(5) }))
    .AddLogging();

if (args.Length > 0 && args[0] == "deploy")
{
    builder.Services.AddHostedService<WorkflowDeployer>();
}
else
{
    builder.Services.AddHostedService<WorkflowWorker>();
}

IHost host = builder.Build();
host.Run();