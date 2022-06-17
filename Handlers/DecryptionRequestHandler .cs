using CAAS.Exceptions;
using CAAS.Models;
using CAAS.Utilities;
using CAAS.Wrappers;
using System.Diagnostics;

namespace CAAS.Handlers
{
    public static class DecryptionRequestHandler
    {
        public static DecryptionResponse Handle(DecryptionRequest _decRequest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            byte[] key = Utils.HexStringToByteArray(_decRequest.HexKey.Replace(" ", ""));
            byte[] cipherData = Utils.HexStringToByteArray(_decRequest.HexCipherData.Replace(" ", ""));
            string algorithm = _decRequest.Algorithm.ToString().Trim().ToLower();
            DecryptionResponse res = new DecryptionResponse();

            byte[] data;
            switch (_decRequest.Algorithm)
            {
                case (SupportedAlgorithms.aes_ebc):
                    data = AesEcbWrapper.Decrypt(cipherData, key);
                    break;
                case (SupportedAlgorithms.aes_cbc):
                    data = AesCbcWrapper.Decrypt(cipherData, key);
                    break;
                default:
                    throw new NotSupportedAlgorithmException(algorithm);


            }

            res.HexData = Utils.ByteArrayToHexString(data);
            stopwatch.Stop();
            res.ProcessingTimeInMs = stopwatch.ElapsedMilliseconds.ToString();
            return res;
        }
    }
}
