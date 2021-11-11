namespace Route256.MerchandiseService.Domain.AggregationModels.ChangeMerchPackFillingRequest
{
    public sealed class RequestNumber
    {
        public RequestNumber(long value)
        {
            Value = value;
        }

        public long Value { get; }
    }
}