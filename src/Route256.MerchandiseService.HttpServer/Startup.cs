using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Route256.MerchandiseService.Server.GrpcServices;
using Route256.MerchandiseService.Server.Infrastructure.Filters;
using Route256.MerchandiseService.Server.Infrastructure.Interceptors;

namespace Route256.MerchandiseService.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc(options => options.Interceptors.Add<UnaryRequestResponseLoggingInterceptor>());
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<MerchApiGrpcService>();
                endpoints.MapControllers();
            });
        }
    }
}