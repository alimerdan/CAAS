using CAAS.Handlers;
using CAAS.Models;
using CAAS.Utilities;
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
                HealthCheckResponse res = HealthChecker.CheckHealth();
                _logger.LogInformation(Utils.GetNow() + "\t-\t" + Request.Path + "\t-\t" + res.ProcessingTimeInMs + " ms");
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(Utils.GetNow() + "\t-\t" + Request.Path + "\t-\t"+ex.Message);
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
                EncryptionResponse res = EncryptionRequestHandler.Handle(encRequest);
                _logger.LogInformation(Utils.GetNow() + "\t-\t" + Request.Path + "\t-\t" + res.ProcessingTimeInMs + " ms");
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(Utils.GetNow() + "\t-\t" + Request.Path + "\t-\t" + ex.Message);
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
                DecryptionResponse res = DecryptionRequestHandler.Handle(decRequest);
                _logger.LogInformation(Utils.GetNow() + "\t-\t" + Request.Path + "\t-\t" + res.ProcessingTimeInMs + " ms");
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(Utils.GetNow() + "\t-\t" + Request.Path + "\t-\t" + ex.Message);
                return BadRequest(new ErrorResponse(ex));
            }
        }

    }
}
