using System.Collections.Generic;
using MediatR;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;

namespace Route256.MerchandiseService.Infrastructure.Commands.CreateMerchPackRequest
{
    /// <summary>
    /// Команда на создание набора мерча
    /// </summary>
    public class CreateMerchPackRequestCommand : IRequest
    {
        public MerchPackName PackName { get; set; }
        public IReadOnlyList<MerchId> MerchIds { get; set; }
    }
}