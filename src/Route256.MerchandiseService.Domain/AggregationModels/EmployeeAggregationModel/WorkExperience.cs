using System.Collections.Generic;
using Route256.MerchandiseService.Domain.Models;

namespace Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel
{
    public class WorkExperience : ValueObject
    {
        public WorkExperience(int value)
        {
            Value = value;
        }
        
        public int Value { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}