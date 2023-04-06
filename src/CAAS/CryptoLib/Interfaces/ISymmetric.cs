namespace CAAS.CryptoLib.Interfaces
{
    public interface ISymmetric
    {
        byte[] Encrypt(byte[] plainBytes, byte[] key, byte[] iv = null);

        byte[] Decrypt(byte[] encryptedBytes, byte[] key, byte[] iv = null);
    }
}
