﻿using CAAS.CryptoLib.Exceptions;
using CAAS.CryptoLib.Interfaces;
using System;
using System.Security.Cryptography;

namespace CAAS.CryptoLib.Algorithms.Symmetric
{
    public class AesCbc : ISymmetric
    {
        private readonly CipherMode mode = CipherMode.CBC;
        public byte[] Encrypt(byte[] plainBytes, byte[] key, byte[] iv = null)
        {
            iv ??= new byte[16];
            try
            {
                Aes aes = GetManagedAes(key, iv);
                ICryptoTransform cipher = aes.CreateEncryptor();
                return cipher.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            }
            catch (Exception e)
            {
                throw new CaaSCryptoException("Couldn't prefrom AES encryption due to error: '" + e.Message + "'");
            }
        }

        public byte[] Decrypt(byte[] encryptedBytes, byte[] key, byte[] iv = null)
        {
            iv ??= new byte[16];
            try
            {
                Aes aes = GetManagedAes(key, iv);
                ICryptoTransform cipher = aes.CreateDecryptor();
                return cipher.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            }
            catch (Exception e)
            {
                throw new CaaSCryptoException("Couldn't prefrom AES encryption due to error: '" + e.Message + "'");
            }
        }

        private Aes GetManagedAes(byte[] key, byte[] iv)
        {
            return new AesManaged
            {
                Key = key,
                IV = iv,
                Mode = mode,
                Padding = PaddingMode.PKCS7
            };
        }
    }
}
