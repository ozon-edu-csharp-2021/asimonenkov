using System.Collections.Generic;
using MediatR;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;

namespace Route256.MerchandiseService.Domain.Events
{
    public class SetMerchPackFillingDomainEvent : INotification
    {
        public MerchPackName MerchPackName { get; }
        private IReadOnlyList<MerchId> MerchPackFilling { get; }

        public SetMerchPackFillingDomainEvent(MerchPackName merchPackName, IReadOnlyList<MerchId> merchPackFilling)
        {
            MerchPackName = merchPackName;
            MerchPackFilling = merchPackFilling;
        }
    }
}