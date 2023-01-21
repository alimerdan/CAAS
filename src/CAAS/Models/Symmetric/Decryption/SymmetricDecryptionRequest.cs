using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Symmetric.Decryption
{
    public class SymmetricDecryptionRequest
    {

        [Required]
        [DefaultValue("C656C652E6656125139C219FD9F6EABB")]
        public string CipherData { get; set; }

        [Required]
        [DefaultValue("aes_cbc")]
        public string Algorithm { get; set; }

        [Required]
        [DefaultValue("00112233445566770011223344556677")]
        public string Key { get; set; }


        [Required]
        [DefaultValue("hex")]
        public string InputDataFormat { get; set; }


        [Required]
        [DefaultValue("hex")]
        public string OutputDataFormat { get; set; }
    }
}
