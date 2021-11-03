using System.Threading;
using System.Threading.Tasks;
using Route256.MerchandiseService.Domain.Contracts;

namespace Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel
{
    /// <summary>
    /// Репозиторий для управления сущностью <see cref="MerchPack"/>
    /// </summary>
    public interface IMerchPackAggregationRepository : IRepository<MerchPack>
    {
        /// <summary>
        /// Получить набор мерча по идентификатору
        /// </summary>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект запроса</returns>
        Task<MerchPack> FindByNameAsync(MerchPackName name, CancellationToken cancellationToken = default);
    }
}