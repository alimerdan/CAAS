using CAAS.Exceptions;
using CAAS.Models;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Text;

namespace CAAS.Utilities
{
    public static class Utils
    {
        public static string GetNow()
        {
            return DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss.fff]");
        }
        public static string ByteArrayToHexString(byte[] bytes)
        {
            return Hex.ToHexString(bytes).ToUpper();
        }

        public static byte[] HexStringToByteArray(string hexVal)
        {
            if (hexVal.Length % 2 != 0)
            {
                hexVal = "0" + hexVal;
            }
            return Hex.Decode(hexVal.ToUpper());
        }
        public static byte[] StringToByteArray(string text, Encoding enc = null)
        {
            if (enc == null)
            {
                enc = Encoding.ASCII;
            }
            byte[] ret = enc.GetBytes(text);
            return ret;
        }

        public static string ByteArrayToString(byte[] data, Encoding enc = null)
        {
            if (enc == null)
            {
                enc = Encoding.ASCII;
            }
            string ret = enc.GetString(data).Replace("\0", "");
            return ret;
        }
        public static byte[] Base64StringToByteArray(string base64EncodedData, Encoding enc = null)
        {
            return Convert.FromBase64String(base64EncodedData);
        }
        public static string ByteArrayToBase64String(byte[] data, Encoding enc = null)
        {
            return Convert.ToBase64String(data);
        }
        public static string EncodeBase64(string plainText, Encoding enc = null)
        {
            if (enc == null)
            {
                enc = Encoding.ASCII;
            }
            byte[] plainTextBytes = enc.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string DecodeBase64(string base64EncodedData, Encoding enc = null)
        {
            if (enc == null)
            {
                enc = Encoding.ASCII;
            }
            byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return enc.GetString(base64EncodedBytes);
        }

        public static string TransformData(string dataFormaValue, byte[] data)
        {
            return DataFormatValues.GetDataFormat(dataFormaValue) switch
            {
                (DataFormat.hex) => Utils.ByteArrayToHexString(data),
                (DataFormat.base64) => Utils.ByteArrayToBase64String(data),
                (DataFormat.ascii) => Utils.ByteArrayToString(data, System.Text.Encoding.ASCII),
                (DataFormat.utf8) => Utils.ByteArrayToString(data, System.Text.Encoding.UTF8),
                _ => throw new NotSupportedDataFormatException(dataFormaValue),
            };
        }
        public static byte[] TransformData(string dataFormaValue, string data)
        {
            return DataFormatValues.GetDataFormat(dataFormaValue) switch
            {
                (DataFormat.hex) => Utils.HexStringToByteArray(data),
                (DataFormat.base64) => Utils.Base64StringToByteArray(data),
                (DataFormat.ascii) => Utils.StringToByteArray(data, System.Text.Encoding.ASCII),
                (DataFormat.utf8) => Utils.StringToByteArray(data, System.Text.Encoding.UTF8),
                _ => throw new NotSupportedDataFormatException(dataFormaValue),
            };
        }
    }
}
