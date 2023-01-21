using CAAS.CryptoLib.Interfaces;
using Org.BouncyCastle.Crypto.Digests;

namespace CAAS.CryptoLib.Algorithms.Hash
{
    public class Sha256 : IHash
    {

        public byte[] Generate(byte[] inputHexByteArray)
        {
            Sha256Digest sha = new Sha256Digest();
            sha.BlockUpdate(inputHexByteArray, 0, inputHexByteArray.Length);
            byte[] hashedByteArray = new byte[sha.GetDigestSize()];
            _ = sha.DoFinal(hashedByteArray, 0);
            return hashedByteArray;
        }
    }
}
