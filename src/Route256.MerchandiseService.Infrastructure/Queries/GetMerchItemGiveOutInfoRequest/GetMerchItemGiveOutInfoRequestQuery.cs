using MediatR;
using Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;

namespace Route256.MerchandiseService.Infrastructure.Queries.GetMerchItemGiveOutInfoRequest
{
    /// <summary>
    /// Запрос на получение информации о выдаче мерча сотруднику
    /// </summary>
    public class GetMerchItemExtraditionInfoRequestQuery : IRequest<Models.Responses.GetMerchItemGiveOutInfoResponse>
    {
        public EmployeeId EmployeeId { get; set; }
        public MerchItem MerchItem { get; set; }
    }
}