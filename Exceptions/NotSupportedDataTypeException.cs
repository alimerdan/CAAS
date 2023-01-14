using System;

namespace CAAS.Exceptions
{
    public class NotSupportedDataTypeException : Exception
    {
        /// <summary>
        /// This Exception occurs when the provided data type is not supported
        /// </summary>
        /// <param name="msg"></param>
        public NotSupportedDataTypeException(string msg = "") : base($"Provided data type is not supported. \"{msg}\"")
        {

        }
    }
}
