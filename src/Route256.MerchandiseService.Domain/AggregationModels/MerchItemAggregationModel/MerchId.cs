using System;
using System.Collections.Generic;
using Route256.MerchandiseService.Domain.Models;

namespace Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel
{
    public sealed class MerchId : ValueObject
    {
        public Guid Value { get; }

        public MerchId(Guid value)
        {
            Value = value;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}