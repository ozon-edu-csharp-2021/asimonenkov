using System;
using System.Collections.Generic;

namespace Route256.MerchandiseService.Infrastructure.Repositories.Models
{
    public class GiveOutMerchItemRequest
    {
        public int Id { get; set; }
        public DateTime GiveOutDate { get; set; }
        public int MerchId { get; set; }
        public int EmployeeId { get; set; }
        public string Status { get; set; }
    }
}