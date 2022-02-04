namespace CAAS.Models
{
    public class EncryptionRequest
    {
        public string HexData { get; set; }
        public string Algorithm { get; set; }
        public string HexKey { get; set; }
    }
}
