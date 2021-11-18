using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.GiveOutMerchPackRequestAggregate;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;
using Route256.MerchandiseService.Infrastructure.Repositories.Interfaces;
using RequestStatus = Route256.MerchandiseService.Domain.AggregationModels.GiveOutMerchItemRequestAggregate.RequestStatus;

namespace Route256.MerchandiseService.Infrastructure.Repositories.Implementations
{
    internal sealed class GiveOutMerchPackAggregationRepository : IGiveOutMerchPackAggregationRepository
    {
        private const int Timeout = 5;
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;

        public GiveOutMerchPackAggregationRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public async Task<GiveOutMerchPackRequest> CreateAsync(GiveOutMerchPackRequest itemToCreate, CancellationToken cancellationToken = default)
        {
            const string script = @"
                INSERT INTO give_out_merch_pack_request (Id, MercPackName, GiveOutDate, Status, EmployeeId)
                VALUES (@Id, @MercPackName, @GiveOutDate, @Status, @EmployeeId);";

            var parameters = new
            {
                Id = itemToCreate.Id,
                MercPackName = itemToCreate.MerchPackName,
                GiveOutDate = itemToCreate.GiveOutDate,
                Status = itemToCreate.RequestStatus,
                EmployeeId = itemToCreate.EmployeeId
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

        public async Task<IReadOnlyList<GiveOutMerchPackRequest>> GetByEmployeeIdAndMerchPackNameAsync(EmployeeId employeeId, MerchPackName merchPackName,
            CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT *
                FROM give_out_merch_item_request
                WHERE EmployeeId = @EmployeeId AND MercPackName = @MercPackName;";
            
            var parameters = new
            {
                EmployeeId = employeeId,
                MercPackName = merchPackName
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var requests = await connection.QueryAsync<GiveOutMerchPackRequest>(commandDefinition);
            var result = requests.Select(x => new GiveOutMerchPackRequest(
                Domain.AggregationModels.GiveOutMerchPackRequestAggregate.RequestStatus.Done, 
                new MerchPackName(x.MerchPackName.Value),
                new EmployeeId(x.EmployeeId.Value)
            )).ToList();
            foreach (var request in result)
            {
                _changeTracker.Track(request);
            }

            return result;
        }
    }
}