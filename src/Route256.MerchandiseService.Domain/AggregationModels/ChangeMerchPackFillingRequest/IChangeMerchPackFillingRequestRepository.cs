using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;
using Route256.MerchandiseService.Domain.Contracts;

namespace Route256.MerchandiseService.Domain.AggregationModels.ChangeMerchPackFillingRequest
{
    /// <summary>
    /// Репозиторий для управления сущностью <see cref="ChangeMerchPackFillingRequest"/>
    /// </summary>
    public interface IChangeMerchPackFillingRequestRepository : IRepository<ChangeMerchPackFillingRequest>
    {
        /// <summary>
        /// Получить запрос по идентификатору
        /// </summary>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект запроса</returns>
        Task<IReadOnlyList<ChangeMerchPackFillingRequest>> FindByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить запрос по названию набора мерча
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект запроса</returns>
        Task<IReadOnlyList<ChangeMerchPackFillingRequest>> FindByNameAsync(MerchPackName name, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Получить запрос по идентификатору
        /// Если запрос не найден, выдает ошибку
        /// </summary>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект запроса</returns>
        Task<IReadOnlyList<ChangeMerchPackFillingRequest>> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить запрос по названию набора мерча
        /// Если запрос не найден, выдает ошибку
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект запроса</returns>
        Task<IReadOnlyList<ChangeMerchPackFillingRequest>> GetByNameAsync(MerchPackName name, CancellationToken cancellationToken = default);
    }
}