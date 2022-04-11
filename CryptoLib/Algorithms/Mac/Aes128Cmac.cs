using CAAS.CryptoLib.Interfaces;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;

namespace CAAS.CryptoLib.Algorithms.Mac
{
    ///<inheritdoc/>
    public class Aes128Cmac : IMac
    {

        ///<inheritdoc/>
        public byte[] Generate(byte[] data, byte[] key)
        {
            AesEngine engine = new AesEngine();
            CMac cmac = new CMac(engine);
            KeyParameter keyParam = new KeyParameter(key);
            cmac.Init(keyParam);
            cmac.BlockUpdate(data, 0, data.Length);
            byte[] outBytes = new byte[16];
            cmac.DoFinal(outBytes, 0);

            return outBytes;
        }
    }
}
