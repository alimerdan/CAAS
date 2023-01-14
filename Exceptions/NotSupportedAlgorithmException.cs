using System;

namespace CAAS.Exceptions
{
    public class NotSupportedAlgorithmException : Exception
    {
        /// <summary>
        /// This Exception occurs when the provided algorithm is not supported
        /// </summary>
        /// <param name="msg"></param>
        public NotSupportedAlgorithmException(string msg = "") : base($"Provided algorithm name is not supported. \"{msg}\"")
        {

        }
    }
}
