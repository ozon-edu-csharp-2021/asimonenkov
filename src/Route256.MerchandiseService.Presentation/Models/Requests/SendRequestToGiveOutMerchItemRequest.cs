using System.Collections.Generic;

namespace Route256.MerchandiseService.Presentation.Models.Requests
{
    public sealed class SendRequestToGiveOutMerchItemRequest
    {
        public string EmployeeId { get; set; }
        public MerchItemRestModel MerchModel { get; set; }
    }
}