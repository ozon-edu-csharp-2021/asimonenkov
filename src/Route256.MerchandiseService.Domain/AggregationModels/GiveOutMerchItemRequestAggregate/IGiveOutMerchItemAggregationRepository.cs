using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Domain.Contracts;

namespace Route256.MerchandiseService.Domain.AggregationModels.GiveOutMerchItemRequestAggregate
{
    /// <summary>
    /// Репозиторий для управления сущностью <see cref="GiveOutMerchItemRequest"/>
    /// </summary>
    public interface IGiveOutMerchItemAggregationRepository : IRepository<GiveOutMerchItemRequest>
    {
        /// <summary>
        /// Получить запросы на выдачу мерча по идентификатору сотрудника и идентификатору мерча
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="merchId"></param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект запроса</returns>
        Task<IReadOnlyList<GiveOutMerchItemRequest> > FindByEmployeeIdAndMerchId(EmployeeId employeeId, MerchId merchId, CancellationToken cancellationToken = default);
        
    }
}