using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Route256.MerchandiseService.Domain.AggregationModels.ChangeMerchPackFillingRequest;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;
using Route256.MerchandiseService.Domain.Contracts;
using Route256.MerchandiseService.Infrastructure.Repositories.Interfaces;

namespace Route256.MerchandiseService.Infrastructure.Repositories.Implementations
{
    internal sealed class ChangeMerchPackFillingRequestRepository : IChangeMerchPackFillingRequestRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public ChangeMerchPackFillingRequestRepository(IChangeTracker changeTracker, IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory)
        {
            _changeTracker = changeTracker;
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<ChangeMerchPackFillingRequest> CreateAsync(ChangeMerchPackFillingRequest itemToCreate, CancellationToken cancellationToken = default)
        {
            const string script = @"
                INSERT INTO change_merch_pack_filling_request (Id, MercPackName, ChangeDate, AdditionalItems)
                VALUES (@Id, @MercPackName, @ChangeDate, @AdditionalItems);";

            var parameters = new
            {
                Id = itemToCreate.Id,
                MercPackName = itemToCreate.MerchPackName,
                ChangeDate = itemToCreate.ChangeDate,
                AdditionalItems = itemToCreate.AdditionalItems
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

        public async Task<IReadOnlyList<ChangeMerchPackFillingRequest>> GetByNameAsync(MerchPackName name, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT *
                FROM change_merch_pack_filling_request
                WHERE MercPackName = @MercPackName;";
            
            var parameters = new
            {
                MercPackName = name
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var requests = await connection.QueryAsync<ChangeMerchPackFillingRequest>(commandDefinition);
            var result = requests.Select(x => new ChangeMerchPackFillingRequest(
                new MerchPackName(x.MerchPackName.ToString()),
                new List<MerchId>(x.AdditionalItems)
            )).ToList();
            foreach (var request in result)
            {
                _changeTracker.Track(request);
            }

            return result;
        }
    }
}