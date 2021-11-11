using System;

namespace Route256.MerchandiseService.Presentation.Exceptions
{
    public sealed class ColourNotExistsException : Exception
    {
        /// <summary>
        /// Исключение, вызываемое, если цвета мерча, переданного в запросе не существует
        /// </summary>
        /// <param name="message"></param>
        public ColourNotExistsException(string message) : base(message)
        {
            
        }
        
        public ColourNotExistsException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}