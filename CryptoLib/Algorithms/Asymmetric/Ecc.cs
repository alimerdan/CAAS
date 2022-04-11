using CAAS.CryptoLib.Interfaces;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace CAAS.CryptoLib.Algorithms.Asymmetric
{
    public class Ecc : IAsymmetric
    {
        public byte[] Decrypt(byte[] cipherBytes, byte[] privateKey, bool withPadding = true)
        {
            throw new System.NotImplementedException();
        }

        public byte[] Encrypt(byte[] plainBytes, byte[] publicKey, bool withPadding = true)
        {
            throw new System.NotImplementedException();
        }

        public byte[] Sign(byte[] data, byte[] privateKey)
        {
            ICipherParameters cipherParameters = PrivateKeyFactory.CreateKey(privateKey);
            ISigner signer = SignerUtilities.GetSigner("SHA256withECDSA");
            signer.Init(true, cipherParameters);
            signer.BlockUpdate(data, 0, data.Length);
            return signer.GenerateSignature();
        }

        public bool Verify(byte[] data, byte[] publicKey, byte[] signature)
        {
            ICipherParameters cipherParameters = PublicKeyFactory.CreateKey(publicKey);
            ISigner signer = SignerUtilities.GetSigner("SHA256withECDSA");
            signer.Init(false, cipherParameters);
            signer.BlockUpdate(data, 0, data.Length);
            return signer.VerifySignature(signature);
        }
    }
}
