namespace CAAS.CryptoLib.Interfaces
{
    /// <summary>
    /// IMac contains method to perform generation of Cbc Mac
    /// </summary>
    public interface IMac
    {

        /// <summary>
        /// Generate is a function responsible for MAC data
        /// </summary>
        /// <param name="data">Data to be processed</param>
        /// <param name="key">Message Key</param>
        /// <returns></returns>
        byte[] Generate(byte[] data, byte[] key);
    }
}
