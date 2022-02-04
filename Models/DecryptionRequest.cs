namespace CAAS.ApiProvider.Models
{
    public class DecryptionRequest
    {
        public string CipherText { get; set; }
        public string Algorithm { get; set; }
        public string Key { get; set; }
    }
}
