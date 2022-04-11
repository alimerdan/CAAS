using CAAS.CryptoLib.Algorithms.Symmetric;

namespace CAAS.Wrappers
{
    public static class AESWrapper
    {
        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            AesEcb x = new AesEcb();
            return x.Encrypt(data, key);
        }
        public static byte[] Decrypt(byte[] cipher, byte[] key)
        {
            AesEcb x = new AesEcb();
            return x.Decrypt(cipher, key);
        }

    }
}
