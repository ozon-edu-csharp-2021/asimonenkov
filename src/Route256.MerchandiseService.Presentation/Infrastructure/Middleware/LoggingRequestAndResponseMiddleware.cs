using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;

namespace Route256.MerchandiseService.Presentation.Infrastructure.Middleware
{
    public class LoggingRequestAndResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingRequestAndResponseMiddleware> _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public LoggingRequestAndResponseMiddleware(RequestDelegate next,
            ILogger<LoggingRequestAndResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await LogRequest(context);
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                await LogResponse(context.Response);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task LogRequest(HttpContext context)
        {
            try
            {
                if (context.Request.ContentLength > 0)
                {
                    context.Request.EnableBuffering();

                    var buffer = new byte[context.Request.ContentLength.Value];
                    var headers = context.Request.Headers.Values;
                    var route = context.Request.Host.Value + context.Request.Path.Value;

                    await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);

                    var bodyAsText = Encoding.UTF8.GetString(buffer);
                    var headersAsJson = JsonSerializer.Serialize(headers);

                    _logger.LogInformation("Request logged");
                    _logger.LogInformation("Headers: {0}", headersAsJson);
                    _logger.LogInformation("Route: {0}", route);
                    _logger.LogInformation("Body: {0}", bodyAsText);

                    context.Request.Body.Position = 0;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log request body");
            }
        }

        private async Task LogResponse(HttpResponse response)
        {
            try
            {
                response.Body.Seek(0, SeekOrigin.Begin);
                var text = await new StreamReader(response.Body).ReadToEndAsync();
                response.Body.Seek(0, SeekOrigin.Begin);
                _logger.LogInformation("Response logged");
                _logger.LogInformation("Body: {0}", text);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log response body");
            }
        }
    }
}