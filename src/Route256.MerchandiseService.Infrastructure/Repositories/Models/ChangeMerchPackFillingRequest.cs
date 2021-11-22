using System;
using System.Collections.Generic;

namespace Route256.MerchandiseService.Infrastructure.Repositories.Models
{
    public class ChangeMerchPackFillingRequest
    {
        public int Id { get; set; }
        public DateTime ChangeDate { get; set; }
        public List<MerchItem> AdditionalItems { get; set; }
        public string MerchPackName { get; set; }
    }
}