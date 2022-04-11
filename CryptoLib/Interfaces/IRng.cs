namespace CAAS.CryptoLib.Interfaces
{
    public interface IRng
    {
        public byte[] GeneratePrng(int size = 16);
    }
}
