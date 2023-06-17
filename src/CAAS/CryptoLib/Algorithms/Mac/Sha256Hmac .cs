using CAAS.CryptoLib.Interfaces;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Linq;

namespace CAAS.CryptoLib.Algorithms.Mac
{
    public class Sha256Hmac : IMac
    {
        public byte[] Generate(byte[] data, byte[] key)
        {
            HMac hmac = new HMac(new Sha256Digest());
            hmac.Init(new KeyParameter(key));
            byte[] result = new byte[hmac.GetMacSize()];

            hmac.BlockUpdate(data, 0, data.Length);
            _ = hmac.DoFinal(result, 0);

            return result;
        }
    }
}
