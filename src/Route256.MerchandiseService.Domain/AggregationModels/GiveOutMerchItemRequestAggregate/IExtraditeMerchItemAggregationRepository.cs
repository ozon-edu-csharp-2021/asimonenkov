using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Domain.Contracts;

namespace Route256.MerchandiseService.Domain.AggregationModels.GiveOutMerchItemRequestAggregate
{
    /// <summary>
    /// Репозиторий для управления сущностью <see cref="ExtraditeMerchItemRequest"/>
    /// </summary>
    public interface IExtraditeMerchItemAggregationRepository : IRepository<ExtraditeMerchItemRequest>
    {
        /// <summary>
        /// Получить запрос на выдачу мерча по идентификатору
        /// </summary>
        /// <param name="number"></param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект запроса</returns>
        Task<IReadOnlyList<ExtraditeMerchItemRequest>> FindByIdAsync(RequestNumber number, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить запросы на выдачу мерча по идентификатору сотрудника и идентификатору мерча
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="merchId"></param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект запроса</returns>
        Task<IReadOnlyList<ExtraditeMerchItemRequest> > FindByEmployeeIdAndMerchId(EmployeeId employeeId, MerchId merchId, CancellationToken cancellationToken = default);
    }
}