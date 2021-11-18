using System;
using System.Collections.Generic;

namespace Route256.MerchandiseService.Infrastructure.Repositories.Models
{
    public class MerchPack
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MerchItem> Items { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}