using System;

namespace Route256.MerchandiseService.Infrastructure.Repositories.Models
{
    public class GiveOutMerchPackRequest
    {
        public int Id { get; set; }
        public DateTime GiveOutDate { get; set; }
        public string MerchPackName { get; set; }
        public int EmployeeId { get; set; }
        public string Status { get; set; }
    }
}