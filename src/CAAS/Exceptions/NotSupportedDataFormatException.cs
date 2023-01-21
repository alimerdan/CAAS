using System;

namespace CAAS.Exceptions
{
    public class NotSupportedDataFormatException : Exception
    {

        public NotSupportedDataFormatException(string msg = "") : base($"Provided data format is not supported. \"{msg}\"")
        {

        }
    }
}
