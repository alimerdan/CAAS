using System;
using System.Runtime.Serialization;

namespace CAAS.CryptoLib.Exceptions
{
    [Serializable]
    public class CaaSCryptoException : Exception
    {
        public CaaSCryptoException(string msg = "") : base($"CAAS\\CryptoException \"{msg}\"")
        {

        }
        protected CaaSCryptoException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
