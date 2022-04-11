using CAAS.CryptoLib.Interfaces;
using Org.BouncyCastle.Crypto.Digests;

namespace CAAS.CryptoLib.Algorithms.Hash
{
    /// <summary>
    /// Sha-256 (Hash Algorithn)
    /// </summary>
    public class Sha256 : IHash
    {
        /// <summary>
        /// Generate Sha-256 hashing algorithm
        /// </summary>
        /// <param name="inputHexByteArray">Hex Byte Array of any length</param>
        /// <returns>byte array of length 64</returns>
        public byte[] Generate(byte[] inputHexByteArray)
        {
            Sha256Digest sha = new Sha256Digest();
            sha.BlockUpdate(inputHexByteArray, 0, inputHexByteArray.Length);
            byte[] hashedByteArray = new byte[sha.GetDigestSize()];
            sha.DoFinal(hashedByteArray, 0);
            return hashedByteArray;
        }
    }
}
