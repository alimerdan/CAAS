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
            string text = DecodeBase64(base64EncodedData, enc);
            return StringToByteArray(text, enc);
        }
        public static string ByteArrayToBase64String(byte[] data, Encoding enc = null)
        {
            string text = ByteArrayToString(data, enc);
            return EncodeBase64(text, enc);
        }
        public static string EncodeBase64(string plainText, Encoding enc = null)
        {
            if (enc == null)
            {
                enc = Encoding.UTF8;
            }
            byte[] plainTextBytes = enc.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string DecodeBase64(string base64EncodedData, Encoding enc = null)
        {
            if (enc == null)
            {
                enc = Encoding.UTF8;
            }
            byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return enc.GetString(base64EncodedBytes);
        }

    }
}
