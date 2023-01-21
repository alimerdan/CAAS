using CAAS.CryptoLib.Interfaces;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using System;

namespace CAAS.CryptoLib.Algorithms.Mac
{
    ///<inheritdoc/>
    public class Aes128Cmac : IMac
    {

        ///<inheritdoc/>
        public byte[] Generate(byte[] data, byte[] key)
        {
            try
            {
                AesEngine engine = new AesEngine();
                CMac cmac = new CMac(engine);
                KeyParameter keyParam = new KeyParameter(key);
                cmac.Init(keyParam);
                cmac.BlockUpdate(data, 0, data.Length);
                byte[] outBytes = new byte[16];
                _ = cmac.DoFinal(outBytes, 0);

                return outBytes;
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't prefrom AES Sign due to error: '" + e.Message + "'"); ;
            }
        }
    }
}
