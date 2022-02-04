using CAAS.ApiProvider.Handlers;
using CAAS.ApiProvider.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAAS.ApiProvider.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CryptoController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CryptoController> _logger;

        public CryptoController(ILogger<CryptoController> logger)
        {
            _logger = logger;
        }

        [HttpPost("encrypt")]
        public ActionResult<EncryptionResponse> Encrypt([FromBody] EncryptionRequest encRequest)
        {
            try
            {
                return Ok(EncryptionRequestHandler.Handle(encRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex));
            }
        }

        [HttpPost("decrypt")]
        public ActionResult<DecryptionResponse> Decrypt([FromBody] DecryptionRequest decRequest)
        {

            try
            {
                return Ok(DecryptionRequestHandler.Handle(decRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex));
            }
        }

    }
}
