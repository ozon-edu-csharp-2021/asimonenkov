using System;

namespace Route256.MerchandiseService.Presentation.Exceptions
{
    public class ClothingSizeNotExistsException : Exception
    {
        /// <summary>
        /// Исключение, вызываемое, если размера одежды, переданного в запросе не существует
        /// </summary>
        /// <param name="message"></param>
        public ClothingSizeNotExistsException(string message) : base(message)
        {
            
        }
        
        public ClothingSizeNotExistsException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}