using System;

namespace Route256.MerchandiseService.Domain.AggregationModels.ChangeMerchPackFillingRequest
{
    public sealed class ChangeDate
    {
        public ChangeDate(DateTime value)
        {
            Value = value;
        }

        public DateTime Value { get; }
    }
}