using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Rng
{

    public class RngResponse
    {

        [Required]
        [DefaultValue("00112233445566770011223344556677")]
        public string Rng { get; set; }

        [Required]
        [DefaultValue(11)]
        public long ProcessingTimeInMs { get; set; }
    }
}
