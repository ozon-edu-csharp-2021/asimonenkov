using System;

namespace Route256.MerchandiseService.Infrastructure.Models.Responses
{
    /// <summary>
    /// Модель ответа на запрос информации о пулочении мерча сотрудником
    /// </summary>
    public class GetMerchItemGiveOutInfoResponse
    {
        public bool HasGiveOut { get; set; }
        public DateTime? GiveOutDate { get; set; }
    }
}