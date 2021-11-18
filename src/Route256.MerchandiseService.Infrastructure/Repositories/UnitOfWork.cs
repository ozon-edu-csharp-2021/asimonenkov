using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Npgsql;
using Route256.MerchandiseService.Domain.Contracts;
using Route256.MerchandiseService.Infrastructure.Repositories.Interfaces;

namespace Route256.MerchandiseService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private NpgsqlTransaction _npgsqlTransaction;
        
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IPublisher _publisher;
        private readonly IChangeTracker _changeTracker;

        public UnitOfWork(
            IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IPublisher publisher,
            IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _publisher = publisher;
            _changeTracker = changeTracker;
        }

        public async ValueTask StartTransaction(CancellationToken token)
        {
            if (_npgsqlTransaction is not null)
            {
                return;
            }
            var connection = await _dbConnectionFactory.CreateConnection(token);
            _npgsqlTransaction = await connection.BeginTransactionAsync(token);
        }

        Task IUnitOfWork.SaveEntitiesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}