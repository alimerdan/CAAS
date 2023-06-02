using System;

namespace CAAS.Exceptions
{
    public class NotSupportedDataFormatForOperationException : Exception
    {
        public NotSupportedDataFormatForOperationException(string dataFormat, string operation) : base($"Provided format \"{dataFormat}\" is not supported for operation \"{operation}\", as it might result to data loss.")
        {

        }
    }
}
