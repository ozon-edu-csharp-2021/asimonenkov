using System.Collections.Generic;

namespace Route256.MerchandiseService.Presentation.Models.Requests
{
    public sealed class GetMerchItemGiveOutInfoRequest
    {
        public string EmployeeId { get; set; }
        public MerchItemRestModel MerchItem { get; set; }
    }


}