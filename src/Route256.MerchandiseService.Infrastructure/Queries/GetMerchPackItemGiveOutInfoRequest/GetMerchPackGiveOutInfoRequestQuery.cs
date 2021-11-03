using MediatR;
using Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;
using Route256.MerchandiseService.Infrastructure.Models.Responses;

namespace Route256.MerchandiseService.Infrastructure.Queries.GetMerchPackItemGiveOutInfoRequest
{
    /// <summary>
    /// Запрос на получение информации о выдаче набора мерча сотруднику
    /// </summary>
    public class GetMerchPackExtraditionInfoRequestQuery : IRequest<GetMerchPackGiveOutInfoResponse>
    {
        public EmployeeId EmployeeId { get; set; }
        public MerchPackName MerchPackName { get; set; }
    }
}