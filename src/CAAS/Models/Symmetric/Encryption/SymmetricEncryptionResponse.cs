using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Symmetric.Encryption
{

    public class SymmetricEncryptionResponse
    {

        [Required]
        [DefaultValue("C656C652E6656125139C219FD9F6EABB")]
        public string CipherData { get; set; }

        [Required]
        [DefaultValue(11)]
        public long ProcessingTimeInMs { get; set; }
    }
}
