using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;
using Route256.MerchandiseService.Infrastructure.Repositories.Interfaces;

namespace Route256.MerchandiseService.Infrastructure.Repositories.Implementations
{
    internal sealed class MerchPackAggregationRepository : IMerchPackAggregationRepository
    {
        private const int Timeout = 5;
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;

        public MerchPackAggregationRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public async Task<MerchPack> CreateAsync(MerchPack itemToCreate, CancellationToken cancellationToken = default)
        {
            const string script = @"
                INSERT INTO merch_pack (Id, ClothingSize, Colour, ItemType)
                VALUES (@Id, @MerchItemIds, @Name);";

            var parameters = new
            {
                Id = itemToCreate.Id,
                MerchItemIds = itemToCreate.PackFilling,
                Name = itemToCreate.PackName
            };
            var commandDefinition = new CommandDefinition(
                script,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
            _changeTracker.Track(itemToCreate);
            return itemToCreate;
        }

        public async Task<MerchPack> GetByNameAsync(MerchPackName name, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT *
                FROM merch_pack
                WHERE Name = @Name;";
            
            var parameters = new
            {
                Name = name
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var requests = await connection.QueryAsync<MerchPack>(commandDefinition);
            var result = requests.Select(x => new MerchPack(
                x.PackName,
                x.PackFilling
            )).FirstOrDefault();
            _changeTracker.Track(result);

            return result;
        }

        public async Task SetMerchPackFillingAsync(MerchPackName name, IReadOnlyList<MerchId> merchPackFilling,
            CancellationToken cancellationToken = default)
        {
            const string sql = @"
               UPDATE merch_pack
               SET MerchItemIds = @MerchItemIds
               WHERE Name = @Name;";
            
            var parameters = new
            {
                Name = name,
                MerchItemIds = merchPackFilling
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
        }
    }
}