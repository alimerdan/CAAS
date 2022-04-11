using CAAS.CryptoLib.Interfaces;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Security;
using System;

namespace CAAS.CryptoLib.Algorithms.Asymmetric
{
    /// <summary>
    /// This class is responsable on all the logic related to the RSA Algorithm.
    /// The RSA in an Encryption/Decryption Algorithm
    /// </summary>
    public class Rsa : IAsymmetric
    {
        public Rsa() { }
        public byte[] Encrypt(byte[] plainData, byte[] keyDer, bool withPadding = true)
        {
            if (withPadding)
            {
                return EncryptWithPadding(plainData, keyDer);
            }
            return Encrypt(plainData, keyDer);
        }
        public byte[] Decrypt(byte[] cipheredData, byte[] keyDer, bool withPadding = true)
        {
            if (withPadding)
            {
                return DecryptWithPadding(cipheredData, keyDer);
            }
            return Decrypt(cipheredData, keyDer);
        }

        private byte[] Encrypt(byte[] plainData, byte[] keyDer)
        {

            AsymmetricKeyParameter publicKey = PublicKeyFactory.CreateKey(keyDer);
            RsaEngine rsaEngineForEncryption = new RsaEngine();
            rsaEngineForEncryption.Init(true, publicKey);
            return rsaEngineForEncryption.ProcessBlock(plainData, 0, plainData.Length);
        }

        private byte[] EncryptWithPadding(byte[] plainData, byte[] keyDer)
        {
            AsymmetricKeyParameter publicKey = PublicKeyFactory.CreateKey(keyDer);
            OaepEncoding encrypter = new OaepEncoding(new RsaEngine(), new Sha256Digest(), new Sha256Digest(), null);
            encrypter.Init(true, publicKey);
            byte[] cipher = encrypter.ProcessBlock(plainData, 0, plainData.Length);
            return cipher;
        }

        private byte[] DecryptWithPadding(byte[] cipheredData, byte[] keyDer)
        {
            AsymmetricKeyParameter privateKey = PrivateKeyFactory.CreateKey(keyDer);
            OaepEncoding decrypter = new OaepEncoding(new RsaEngine(), new Sha256Digest(), new Sha256Digest(), null);
            decrypter.Init(false, privateKey);
            byte[] plainData = decrypter.ProcessBlock(cipheredData, 0, cipheredData.Length);
            return plainData;
        }

        private byte[] Decrypt(byte[] cipheredData, byte[] keyDer)
        {
            AsymmetricKeyParameter privateKey = PrivateKeyFactory.CreateKey(keyDer);
            RsaEngine rsaEngineForDecryption = new RsaEngine();
            rsaEngineForDecryption.Init(false, privateKey);
            return rsaEngineForDecryption.ProcessBlock(cipheredData, 0, cipheredData.Length);
        }

        public byte[] Sign(byte[] data, byte[] privateKey)
        {
            throw new NotImplementedException();
        }

        public bool Verify(byte[] data, byte[] publicKey, byte[] signature)
        {
            throw new NotImplementedException();
        }
    }
}
