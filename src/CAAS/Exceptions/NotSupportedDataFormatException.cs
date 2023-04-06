using System;
using System.Runtime.Serialization;

namespace CAAS.Exceptions
{
    [Serializable]
    public class NotSupportedDataFormatException : Exception
    {

        public NotSupportedDataFormatException(string msg = "") : base($"Provided data format is not supported. \"{msg}\"")
        {

        }
        protected NotSupportedDataFormatException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
