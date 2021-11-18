using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Domain.Contracts;

namespace Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel
{
    /// <summary>
    /// Репозиторий для управления сущностью <see cref="MerchPack"/>
    /// </summary>
    public interface IMerchPackAggregationRepository : IRepository<MerchPack>
    {
        /// <summary>
        /// Получить набор мерча по названию
        /// Если мерч не найден, выдает ошибку
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект запроса</returns>
        Task<MerchPack> GetByNameAsync(MerchPackName name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить набор мерча по названию
        /// Если мерч не найден, выдает ошибку
        /// </summary>
        /// <param name="name"></param>
        /// <param name="merchPackFilling"></param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект запроса</returns>
        Task SetMerchPackFillingAsync(MerchPackName name, IReadOnlyList<MerchId> merchPackFilling, CancellationToken cancellationToken = default);
    }
}