using CAAS.ApiProvider.Models;
using System;

namespace CAAS.ApiProvider.Handlers
{
    public static class EncryptionRequestHandler
    {
        public static EncryptionResponse Handle(EncryptionRequest _encRequest)
        {
            EncryptionResponse res = new EncryptionResponse(); ;
            res.CipherText = CAAS.Utilities.SupportedOperations.Symmetric.aes.encrypt();
            return res;
        }
    }
}
