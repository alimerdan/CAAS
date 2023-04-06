using System;
using System.Runtime.Serialization;

namespace CAAS.Exceptions
{
    [Serializable]
    public class NotSupportedAlgorithmException : Exception
    {
        public NotSupportedAlgorithmException(string msg = "") : base($"Provided algorithm name is not supported. \"{msg}\"")
        {

        }

        protected NotSupportedAlgorithmException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
