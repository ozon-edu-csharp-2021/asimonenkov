using System;

namespace Route256.MerchandiseService.Domain.AggregationModels.ChangeMerchPackFillingRequest
{
    public class ChangeDate
    {
        public ChangeDate(DateTime value)
        {
            Value = value;
        }

        public DateTime Value { get; }
    }
}