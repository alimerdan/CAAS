using CAAS.Exceptions;
using CAAS.Models;
using CAAS.Utilities;
using CAAS.Wrappers;

namespace CAAS.Handlers
{
    public static class EncryptionRequestHandler
    {
        public static EncryptionResponse Handle(EncryptionRequest _encRequest)
        {
            byte[] key = Utils.HexStringToByteArray(_encRequest.HexKey);
            byte[] data = Utils.HexStringToByteArray(_encRequest.HexData);
            string algorithm = _encRequest.Algorithm.Trim().ToLower();
            EncryptionResponse res = new EncryptionResponse();
            byte[] cipher;
            switch (algorithm)
            {
                case ("aes"):
                    cipher = AESWrapper.Encrypt(data, key);
                    break;
                default:
                    throw new NotSupportedAlgorithmException(algorithm);


            }
            res.HexCipherData = Utils.ByteArrayToHexString(AESWrapper.Encrypt(data, key));
            return res;
        }
    }
}
