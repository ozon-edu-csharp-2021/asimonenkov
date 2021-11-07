using System.Collections.Generic;
using Route256.MerchandiseService.Domain.Models;

namespace Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel
{
    public sealed class MerchPackName : ValueObject
    {
        public string Value { get; }

        public MerchPackName(string value)
        {
            Value = value;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}