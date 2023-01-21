using System;

namespace CAAS.CryptoLib.Exceptions
{
    public class InvalidEccKeySizeException : Exception
    {
        /// <summary>
        /// This Exception occurs when the RSA key size is not supported
        /// </summary>
        /// <param name="msg"></param>
        public InvalidEccKeySizeException(string msg = "") : base($"The Ecc key size is invalid. Typical Ecc key sizes are 256 or 384 bits.\n {msg}")
        {

        }
    }
}
