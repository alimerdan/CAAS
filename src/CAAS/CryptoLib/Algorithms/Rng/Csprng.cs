using CAAS.CryptoLib.Interfaces;
using System.Security.Cryptography;
namespace CAAS.CryptoLib.Algorithms.Rng
{
    public class Csprng : IRng
    {
        public byte[] GeneratePrng(int size = 16)
        {
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            byte[] csprng = new byte[size];
            rng.GetBytes(csprng);
            return csprng;
        }
    }
}
