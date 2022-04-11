using System;

namespace CAAS.CryptoLib.Exceptions
{
    public class InvalidRsaKeySizeException : Exception
    {
        /// <summary>
        /// This Exception occurs when the RSA key size is not supported
        /// </summary>
        /// <param name="msg"></param>
        public InvalidRsaKeySizeException(string msg = "") : base($"The Rsa key size is invalid. Typical RSA key sizes are 1,024 or 2,048 or 3,072 or 4,096 bits.\n {msg}")
        {

        }
    }
}
