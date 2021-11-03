namespace Route256.MerchandiseService.Domain.AggregationModels.ChangeMerchPackFillingRequest
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