namespace CAAS.CryptoLib.Interfaces
{
    public interface IAsymmetric
    {
        byte[] Encrypt(byte[] plainBytes, byte[] publicKey, bool withPadding = true);
        byte[] Decrypt(byte[] cipherBytes, byte[] privateKey, bool withPadding = true);
        byte[] Sign(byte[] data, byte[] privateKey);
        bool Verify(byte[] data, byte[] publicKey, byte[] signature);
    }
}
