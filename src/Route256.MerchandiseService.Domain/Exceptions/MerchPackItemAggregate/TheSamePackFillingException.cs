using System;

namespace Route256.MerchandiseService.Domain.Exceptions.MerchPackItemAggregate
{
    public class TheSamePackFillingException : Exception
    {
        public TheSamePackFillingException(string message) : base(message)
        {
            
        }
        
        public TheSamePackFillingException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}