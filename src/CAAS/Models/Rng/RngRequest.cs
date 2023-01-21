using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Rng
{

    public class RngRequest
    {


        [Required]
        [DefaultValue("csprng")]
        public string Algorithm { get; set; }


        [Required]
        [DefaultValue(16)]
        public int Size { get; set; }


        [Required]
        [DefaultValue("hex")]
        public string OutputDataFormat { get; set; }
    }
}
