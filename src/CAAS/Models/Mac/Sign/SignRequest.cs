using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Mac.Sign
{

    public class SignRequest
    {

        [Required]
        [DefaultValue("0011223344556677")]
        public string Data { get; set; }

        [Required]
        [DefaultValue("aes128_cmac")]
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
