namespace Route256.MerchandiseService.Domain.AggregationModels.GiveOutMerchPackRequestAggregate
{
    public class RequestNumber
    {
        public RequestNumber(long value)
        {
            Value = value;
        }

        public long Value { get; }
    }
}