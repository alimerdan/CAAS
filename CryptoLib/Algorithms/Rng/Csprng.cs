using CAAS.CryptoLib.Interfaces;
using System.Security.Cryptography;
namespace CAAS.CryptoLib.Algorithms.Rng
{
    /// <summary>
    /// Cryptographically Secure Pseudorandom Number Generator
    /// </summary>
    public class Csprng : IRng
    {
        /// <summary>
        /// Genrate Random Number with the specified length
        /// </summary>
        /// <param name="size"></param>
        /// <returns>byte array contains the random number</returns>
        public byte[] GeneratePrng(int size = 16)
        {
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            byte[] csprng = new byte[size];
            rng.GetBytes(csprng);
            return csprng;
        }
    }
}
