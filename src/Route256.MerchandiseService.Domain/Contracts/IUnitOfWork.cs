using System.Threading;
using System.Threading.Tasks;

namespace Route256.MerchandiseService.Domain.Contracts
{
    public interface IUnitOfWork
    {
        ValueTask StartTransaction(CancellationToken cancellationToken = default);
        Task SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}