using System;

namespace Route256.MerchandiseService.Domain.Exceptions.MerchPackItemAggregate
{
    /// <summary>
    /// Исключение, вызывающееся, в случае, если в наборе количество товаров больше одного, но при этом ему на задано имя
    /// </summary>
    public class NoPackNameException : Exception
    {
        public NoPackNameException(string message) : base(message)
        {
            
        }
        
        public NoPackNameException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}