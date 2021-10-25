using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Route256.MerchandiseService.Server.Infrastructure.Middleware
{
    public class VersionMiddleware
    {
        public VersionMiddleware(RequestDelegate next)
        {
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "no version";
            var serviceName = Assembly.GetExecutingAssembly().GetName().Name?.Split('.').Length > 1 ? Assembly.GetExecutingAssembly().GetName().Name?.Split('.')[1] : "no service name";
            await context.Response.WriteAsync($"version: {version}, serviceName: {serviceName}");
        }
    }
}