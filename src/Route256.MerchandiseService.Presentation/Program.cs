using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Route256.MerchandiseService.Presentation.Infrastructure.Extensions;

namespace Route256.MerchandiseService.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .AddInfrastructure()
                .AddHttp();
    }
}