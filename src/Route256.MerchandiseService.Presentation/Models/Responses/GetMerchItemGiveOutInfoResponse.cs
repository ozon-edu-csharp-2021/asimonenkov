using System;

namespace Route256.MerchandiseService.Presentation.Models.Responses
{
    public sealed class GetMerchItemGiveOutInfoResponse
    {
        public bool HasGiveOut { get; set; }
        public DateTime GiveOutDate { get; set; }
    }
}