using System.Collections.Generic;
using MediatR;
using Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;

namespace Route256.MerchandiseService.Infrastructure.Commands.GiveOutMerchItemRequest
{
    /// <summary>
    /// Команда на выдачу мерча
    /// </summary>
    public sealed class GiveOutMerchItemsRequestCommand : IRequest
    {
        public EmployeeId EmployeeId { get; set; }
        public IReadOnlyList<MerchItem> MerchItems { get; set; }
    }
}