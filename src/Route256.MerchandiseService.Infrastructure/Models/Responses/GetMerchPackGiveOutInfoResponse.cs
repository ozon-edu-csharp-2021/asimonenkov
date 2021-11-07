using System;
using System.Collections.Generic;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;

namespace Route256.MerchandiseService.Infrastructure.Models.Responses
{
    /// <summary>
    /// Модель ответа на запрос информации о пулочении набора мерча сотрудником
    /// </summary>
    public sealed class GetMerchPackGiveOutInfoResponse
    {
        public bool HasGiveOut { get; set; }
        public DateTime? GiveOutDate { get; set; }
        public IReadOnlyList<MerchItem> MerchNeedToGiveOut { get; set; }
    }
}