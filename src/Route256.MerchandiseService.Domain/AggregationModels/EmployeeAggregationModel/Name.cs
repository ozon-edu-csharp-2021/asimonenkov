using System.Collections.Generic;
using Route256.MerchandiseService.Domain.Models;

namespace Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel
{
    public sealed class Name : ValueObject
    {
        public Name(string value)
        {
            Value = value;
        }
        
        public string Value { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}