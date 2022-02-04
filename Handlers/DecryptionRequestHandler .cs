using CAAS.ApiProvider.Models;
using System;

namespace CAAS.ApiProvider.Handlers
{
    public static class DecryptionRequestHandler
    {
        public static DecryptionResponse Handle(DecryptionRequest _decRequest)
        {
            DecryptionResponse res = new DecryptionResponse(); ;
            res.PlainText = CAAS.Utilities.SupportedOperations.Symmetric.aes.decrypt();
            return res;
        }
    }
}
