using MediatR;
using Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;

namespace Route256.MerchandiseService.Infrastructure.Commands.GiveOutMerchPackRequest
{
    /// <summary>
    /// Команда на выдачу набора мерча
    /// </summary>
    public sealed class GiveOutMerchPackRequestCommand : IRequest
    {
        public MerchPackName MerchPackName { get; set; }
        public EmployeeId EmployeeId { get; set; }
    }
}