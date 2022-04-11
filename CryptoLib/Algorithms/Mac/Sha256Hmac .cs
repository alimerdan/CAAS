using CAAS.CryptoLib.Interfaces;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;


namespace CAAS.CryptoLib.Algorithms.Mac
{
    /// <summary>
    /// Generate HMAC SHA256
    /// </summary>
    public class Sha256Hmac : IMac
    {
        /// <summary>
        /// Generate HMAC (hash-based message authentication code) using a cryptographic hash function and a secret cryptographic key.
        /// </summary>
        /// <param name="data">byte array of the data</param>
        /// <param name="key">byte array of the key</param>
        /// <returns>byte array of the hashed value of length 64</returns>
        public byte[] Generate(byte[] data, byte[] key)
        {
            HMac hmac = new HMac(new Sha256Digest());
            hmac.Init(new KeyParameter(key));
            byte[] result = new byte[hmac.GetMacSize()];

            hmac.BlockUpdate(data, 0, data.Length);
            hmac.DoFinal(result, 0);

            return result;
        }
    }
}
