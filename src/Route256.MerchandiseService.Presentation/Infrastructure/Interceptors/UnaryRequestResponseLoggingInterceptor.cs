using System;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace Route256.MerchandiseService.Presentation.Infrastructure.Interceptors
{
    public class UnaryRequestResponseLoggingInterceptor : Interceptor
    {
        private readonly ILogger<UnaryRequestResponseLoggingInterceptor> _logger;

        public UnaryRequestResponseLoggingInterceptor(ILogger<UnaryRequestResponseLoggingInterceptor> logger)
        {
            _logger = logger;
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                var requestJson = JsonSerializer.Serialize(request);
                _logger.LogInformation("Grpc request logged: {0}", requestJson);

                var response = base.UnaryServerHandler(request, context, continuation);

                var responseJson = JsonSerializer.Serialize(response);
                _logger.LogInformation("Grpc response logged: {0}", responseJson);

                return response;
            }

            catch (Exception e)
            {
                _logger.LogError("Cannot log request or result \nReason: {0}", e.Message);
            }

            return null;
        }
    }
}