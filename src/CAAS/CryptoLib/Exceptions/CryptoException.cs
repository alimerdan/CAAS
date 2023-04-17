using System;

namespace CAAS.CryptoLib.Exceptions
{
    public class CaaSCryptoException : Exception
    {
        public CaaSCryptoException(string msg = "") : base($"CAAS\\CryptoException \"{msg}\"")
        {

        }
    }
}
