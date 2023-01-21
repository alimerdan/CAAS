using System;

namespace CAAS.Exceptions
{
    public class NotSupportedAlgorithmException : Exception
    {
        public NotSupportedAlgorithmException(string msg = "") : base($"Provided algorithm name is not supported. \"{msg}\"")
        {

        }

    }
}
