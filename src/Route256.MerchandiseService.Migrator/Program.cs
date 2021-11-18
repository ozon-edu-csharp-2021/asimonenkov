using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FluentMigrator.Runner;
using Npgsql;

namespace Route256.MerchandiseService.Migrator
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            
            var connectionString = configuration.GetSection("DatabaseConnectionOptions:ConnectionString").Get<string>();
            var services = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(
                    rb => rb
                        .AddPostgres()
                        .WithGlobalConnectionString(connectionString)
                        .ScanIn(typeof(Program).Assembly)
                        .For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());
            
            var serviceProvider = services.BuildServiceProvider(false);

            using (serviceProvider.CreateScope())
            {
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
                if (args.Contains("--dryrun"))
                {
                    runner.ListMigrations();
                }
                else
                {
                    runner.MigrateUp();
                }

                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                connection.ReloadTypes();
            }
        }
    }
}