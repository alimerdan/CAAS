using CAAS.CryptoLib.Algorithms.Hash;
using CAAS.CryptoLib.Interfaces;
using CAAS.Exceptions;
using CAAS.Models;
using CAAS.Models.Hash;
using CAAS.Utilities;
using System.Diagnostics;

namespace CAAS.Handlers.Hash
{
    public static class HashRequestHandler
    {
        public static HashResponse Handle(HashRequest _hashRequest,CAASDataType _dataType, out CAASDataType retDataType)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string algorithm = _hashRequest.Algorithm.ToString().Trim().ToLower();
            HashResponse res = new HashResponse();
            byte[] data = _dataType switch
            {
                (CAASDataType.hex) => Utils.HexStringToByteArray(_hashRequest.Data.Replace(" ", "")),
                (CAASDataType.base64) => Utils.Base64StringToByteArray(_hashRequest.Data),
                (CAASDataType.text) => Utils.StringToByteArray(_hashRequest.Data),
                _ => throw new NotSupportedDataTypeException(_dataType.ToString()),
            };
            IHash processor = _hashRequest.Algorithm switch
            {
                HashSupportedAlgorithms.sha256 => new Sha256(),
                _ => throw new NotSupportedAlgorithmException(algorithm),
            };
            byte[] digestedData = processor.Generate(data);
            switch (_dataType)
            {
                case (CAASDataType.hex):
                case (CAASDataType.text):
                    retDataType= CAASDataType.hex;
                    break;
                case (CAASDataType.base64):
                    retDataType = CAASDataType.base64;
                        break;
                default:
                    throw new NotSupportedDataTypeException(_dataType.ToString());
            }
            res.Digest = retDataType switch
            {
                (CAASDataType.hex) => Utils.ByteArrayToHexString(digestedData),
                (CAASDataType.base64) => Utils.ByteArrayToBase64String(digestedData),
                _ => throw new NotSupportedDataTypeException(_dataType.ToString()),
            };
            stopwatch.Stop();
            res.ProcessingTimeInMs = stopwatch.ElapsedMilliseconds.ToString();
            return res;
        }
    }
}
