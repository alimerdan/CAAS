using Org.BouncyCastle.Utilities.Encoders;
using System.Text;

namespace CAAS.CryptoLib.CryptoAlgorithms
{
    public class Utilities
    {
        public static string ByteArrayToHexString(byte[] bytes)
        {
            return Hex.ToHexString(bytes);
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
    }
}
