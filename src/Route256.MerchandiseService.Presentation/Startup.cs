using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Route256.MerchandiseService.Presentation.GrpcServices;
using Route256.MerchandiseService.Presentation.Infrastructure.Interceptors;

namespace Route256.MerchandiseService.Presentation
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