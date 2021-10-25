using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Route256.MerchandiseService.Server.Infrastructure.Filters;
using Route256.MerchandiseService.Server.Infrastructure.StartupFilters;

namespace Route256.MerchandiseService.Server.Infrastructure.Extensions
{
    public static class HostBuilderExtension
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter, TerminalStartupFilter>();
                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();
                services.AddSingleton<IStartupFilter, LiveStartupFilter>();
                services.AddSingleton<IStartupFilter, ReadyStartupFilter>();
                
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "Route256.MerchandiseService", Version = "v1"});
                });
            });
            return builder;
        }
        
        public static IHostBuilder AddHttp(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());
            });
            
            return builder;
        }
    }
}