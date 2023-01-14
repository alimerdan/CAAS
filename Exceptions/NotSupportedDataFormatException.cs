using System;

namespace CAAS.Exceptions
{
    public class NotSupportedDataFormatException : Exception
    {
        /// <summary>
        /// This Exception occurs when the provided data type is not supported
        /// </summary>
        /// <param name="msg"></param>
        public NotSupportedDataFormatException(string msg = "") : base($"Provided data format is not supported. \"{msg}\"")
        {

        }
    }
}
