namespace Niobium.Ads.MCP
{
    public class ApiKeyMiddleware(RequestDelegate next)
    {
        private const string API_KEY_HEADER_NAME = "X-Api-Key"; // The expected header name

        public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
        {
            if (!context.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("API Key was not provided.");
                return;
            }

            var apikey = Environment.GetEnvironmentVariable("X_API_KEY");
            if (apikey == null)
            {
                context.Response.StatusCode = 500; // Unauthorized
                await context.Response.WriteAsync("API Key is not configured.");
                return;
            }

            if (!apikey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Unauthorized client.");
                return;
            }

            await next(context);
        }
    }
}
