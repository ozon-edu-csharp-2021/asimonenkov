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
    internal sealed class MerchItemAggregationRepository : IMerchItemAggregationRepository
    {
        private const int Timeout = 5;
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;

        public MerchItemAggregationRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }
        public async Task<MerchItem> CreateAsync(MerchItem itemToCreate, CancellationToken cancellationToken = default)
        {
            const string script = @"
                INSERT INTO merch_item (Id, ClothingSize, Colour, ItemType)
                VALUES (@Id, @ClothingSize, @Colour, @ItemType);";

            var parameters = new
            {
                Id = itemToCreate.Id,
                ClothingSize = itemToCreate.ClothingSize,
                Colour = itemToCreate.Colour,
                ItemType = itemToCreate.ItemType
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

        public async Task<MerchItem> FindByIdAsync(MerchId id, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT *
                FROM merch_pack
                WHERE Id = @Id;";
            
            var parameters = new
            {
                Id = id
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var requests = await connection.QueryAsync<MerchItem>(commandDefinition);
            var result = requests.Select(x => new MerchItem(
               new Item(x.ItemType.Type),
               x.ClothingSize,
               x.Colour
            )).FirstOrDefault();
            _changeTracker.Track(result);

            return result;
        }

        public async Task<MerchItem> GetByProperties(Item type, Colour colour, ClothingSize? size, CancellationToken cancellationToken = default)
        {
            string sql = "";
            if (size is not null)
            {
                sql = @"
                SELECT *
                FROM merch_pack
                WHERE ItemType = @ItemType AND Colour = @Colour AND ClothingSize = @ClothingSize;";
            }

            else
            {
                sql = @"
                SELECT *
                FROM merch_pack
                WHERE ItemType = @ItemType AND Colour = @Colour";
            }
            
            var parameters = new
            {
                ItemType = type,
                Colour = colour,
                Size = size
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var requests = await connection.QueryAsync<MerchItem>(commandDefinition);
            var result = requests.Select(x => new MerchItem(
                new Item(x.ItemType.Type),
                x.ClothingSize,
                x.Colour
            )).FirstOrDefault();
            _changeTracker.Track(result);

            return result;
        }
    }
}