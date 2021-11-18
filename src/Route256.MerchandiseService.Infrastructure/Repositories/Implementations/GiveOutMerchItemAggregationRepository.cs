using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.GiveOutMerchItemRequestAggregate;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Infrastructure.Repositories.Interfaces;

namespace Route256.MerchandiseService.Infrastructure.Repositories.Implementations
{
    internal sealed class GiveOutMerchItemAggregationRepository : IGiveOutMerchItemAggregationRepository
    {
        private const int Timeout = 5;
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;

        public GiveOutMerchItemAggregationRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public async Task<GiveOutMerchItemRequest> CreateAsync(GiveOutMerchItemRequest itemToCreate, CancellationToken cancellationToken = default)
        {
            const string script = @"
                INSERT INTO give_out_merch_item_request (Id, MercId, GiveOutDate, Status, EmployeeId)
                VALUES (@Id, @MercId, @GiveOutDate, @Status, @EmployeeId);";

            var parameters = new
            {
                Id = itemToCreate.Id,
                MercId = itemToCreate.MerchId,
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

        public async Task<IReadOnlyList<GiveOutMerchItemRequest>> FindByEmployeeIdAndMerchId(EmployeeId employeeId, MerchId merchId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT *
                FROM give_out_merch_item_request
                WHERE EmployeeId = @EmployeeId AND MercId = @MercId;";
            
            var parameters = new
            {
                EmployeeId = employeeId,
                MercId = merchId
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var requests = await connection.QueryAsync<GiveOutMerchItemRequest>(commandDefinition);
            var result = requests.Select(x => new GiveOutMerchItemRequest(
                RequestStatus.Done,
                new MerchId(x.MerchId.Value),
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