using System;

namespace Route256.MerchandiseService.Domain.AggregationModels.GiveOutMerchItemRequestAggregate
{
    public sealed class GiveOutDate
    {
        public GiveOutDate(DateTime value)
        {
            Value = value;
        }

        public DateTime Value { get; }
    }
}