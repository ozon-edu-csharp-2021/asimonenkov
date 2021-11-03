using System.Collections.Generic;
using MediatR;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;

namespace Route256.MerchandiseService.Infrastructure.Commands.ChangePackFillingRequest
{
    /// <summary>
    /// Команда на изменение состава набора мерча
    /// </summary>
    public class ChangePackFillingRequestCommand : IRequest
    {
        public MerchPackName PackName { get; set; }
        public IReadOnlyList<MerchId> MerchIds { get; set; }
    }
}