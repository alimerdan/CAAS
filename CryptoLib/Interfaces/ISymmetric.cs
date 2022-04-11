namespace CAAS.CryptoLib.Interfaces
{
    public interface ISymmetric
    {
        byte[] Encrypt(byte[] bytes, byte[] key, byte[] iv = null);

        byte[] Decrypt(byte[] bytes, byte[] key, byte[] iv = null);
    }
}
