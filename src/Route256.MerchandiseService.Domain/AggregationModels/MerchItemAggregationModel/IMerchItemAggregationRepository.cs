#nullable enable
using System.Threading;
using System.Threading.Tasks;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;
using Route256.MerchandiseService.Domain.Contracts;

namespace Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel
{
    /// <summary>
    /// Репозиторий для управления сущностью <see cref="MerchItem"/>
    /// </summary>
    public interface IMerchItemAggregationRepository : IRepository<MerchItem>
    {
        /// <summary>
        /// Получить мерч по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект запроса</returns>
        Task<MerchItem> FindByIdAsync(MerchId id, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Получить мерч по его свойствам
        /// Если мерч не найден, выдает ошибку
        /// </summary>
        /// <param name="type"></param>
        /// <param name="colour"></param>
        /// <param name="size"></param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект запроса</returns>
        Task<MerchItem> GetByProperties(Item type, Colour colour, ClothingSize? size, CancellationToken cancellationToken = default);
    }
}