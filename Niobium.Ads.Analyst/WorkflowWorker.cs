using Microsoft.Extensions.Hosting;

namespace Niobium.Ads.Analyst
{
    internal class WorkflowWorker(AnalystWorkflow workflow) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var conversationID = Guid.NewGuid();
            await workflow.RunAsync(conversationID.ToString(), cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
