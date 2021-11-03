using System.Collections.Generic;

namespace Route256.MerchandiseService.Presentation.Models.Requests
{
    public sealed class GetMerchPackGiveOutInfoRequest
    {
        public string EmployeeId { get; set; }
        public string MerchPackName { get; set; }
    }
}