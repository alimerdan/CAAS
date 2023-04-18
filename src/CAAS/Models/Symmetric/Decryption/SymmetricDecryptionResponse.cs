using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Symmetric.Decryption
{
    public class SymmetricDecryptionResponse
    {

        [Required]
        [DefaultValue("0011223344556677")]
        public string Data { get; set; }

        [Required]
        [DefaultValue(11)]
        public double ProcessingTimeInMs { get; set; }
    }
}
