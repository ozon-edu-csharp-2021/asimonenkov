using System;
using System.Collections.Generic;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;

namespace Route256.MerchandiseService.Presentation.Models.Responses
{
    public sealed class GetMerchPackGiveOutInfoResponse
    {
        public bool HasGiveOut { get; set; }
        public DateTime GiveOutDate { get; set; }
        public IReadOnlyList<MerchItem> MerchItemsNeedToGiveOut { get; set; }
    }
}