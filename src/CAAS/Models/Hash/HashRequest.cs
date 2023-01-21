using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Hash
{

    public class HashRequest
    {

        [Required]
        [DefaultValue("0011223344556677")]
        public string Data { get; set; }

        [Required]
        [DefaultValue("sha256")]
        public string Algorithm { get; set; }


        [Required]
        [DefaultValue("hex")]
        public string InputDataFormat { get; set; }

        [Required]
        [DefaultValue("hex")]
        public string OutputDataFormat { get; set; }
    }
}
