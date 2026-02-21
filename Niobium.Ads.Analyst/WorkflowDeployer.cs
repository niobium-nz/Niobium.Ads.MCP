using Microsoft.Extensions.Hosting;

namespace Niobium.Ads.Analyst
{
    internal class WorkflowDeployer(AnalystWorkflow workflow) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken) => await workflow.DeployAsync(cancellationToken);

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
