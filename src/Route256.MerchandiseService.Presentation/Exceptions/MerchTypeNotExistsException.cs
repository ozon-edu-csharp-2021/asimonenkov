using System;

namespace Route256.MerchandiseService.Presentation.Exceptions
{
    public sealed class MerchTypeNotExistsException : Exception
    {
        /// <summary>
        /// Исключение, вызываемое, если типа мерча, переданного в запросе не существует
        /// </summary>
        /// <param name="message"></param>
        public MerchTypeNotExistsException(string message) : base(message)
        {
            
        }
        
        public MerchTypeNotExistsException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}