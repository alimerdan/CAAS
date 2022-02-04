using CAAS.Handlers;
using CAAS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CAAS.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CryptoController : ControllerBase
    {

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
