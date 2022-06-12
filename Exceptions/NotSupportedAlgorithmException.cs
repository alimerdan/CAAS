using System;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Exceptions
{
    public class NotSupportedAlgorithmException : Exception
    {
        /// <summary>
        /// This Exception occurs when the RSA key size is not supported
        /// </summary>
        /// <param name="msg"></param>
        public NotSupportedAlgorithmException(string msg = "") : base($"Provided algorithm name is not supported. \"{msg}\"")
        {

        }
    }
}
