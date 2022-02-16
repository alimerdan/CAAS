using System.ComponentModel.DataAnnotations;

namespace CAAS.Models
{
    public class HealthCheckResponse
    {
        [Required]
        public string Status { get; set; }
        [Required]
        public string ProcessingTimeInMs { get; set; }
    }
}
