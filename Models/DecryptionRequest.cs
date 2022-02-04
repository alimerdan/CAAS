namespace CAAS.Models
{
    public class DecryptionRequest
    {
        public string HexCipherData { get; set; }
        public string Algorithm { get; set; }
        public string HexKey { get; set; }
    }
}
