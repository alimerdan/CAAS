namespace CAAS.CryptoLib.Interfaces
{
    public interface IHash
    {
        public byte[] Generate(byte[] inputHexByteArray);
    }
}
