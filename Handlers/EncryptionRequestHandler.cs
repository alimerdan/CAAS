using CAAS.Exceptions;
using CAAS.Models;
using CAAS.Utilities;
using CAAS.Wrappers;
using System.Diagnostics;
using System.Threading;

namespace CAAS.Handlers
{
    public static class EncryptionRequestHandler
    {
        public static EncryptionResponse Handle(EncryptionRequest _encRequest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            byte[] key = Utils.HexStringToByteArray(_encRequest.HexKey.Replace(" ",""));
            byte[] data = Utils.HexStringToByteArray(_encRequest.HexData.Replace(" ",""));
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
            stopwatch.Stop();
            res.ProcessingTimeInMs = stopwatch.ElapsedMilliseconds.ToString();
            return res;
        }
    }
}
