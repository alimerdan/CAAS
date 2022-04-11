using CAAS.Handlers;
using CAAS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CAAS.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class CryptoController : ControllerBase
    {

        private readonly ILogger<CryptoController> _logger;

        public CryptoController(ILogger<CryptoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("~/api/v1/health")]
        public ActionResult<HealthCheckResponse> Get()
        {
            try
            {
                return Ok(HealthChecker.CheckHealth());
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex));
            }
        }

        [HttpPost("encrypt")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(EncryptionResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
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
        [Consumes("application/json")]
        [ProducesResponseType(typeof(DecryptionResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
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
