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
                _logger.LogInformation($"{Utils.GetNow()} \t-\t {Request.Path} \t-\t {Request.ContentLength} bytes");
                HealthCheckResponse res = HealthChecker.CheckHealth();
                _logger.LogInformation($"{Utils.GetNow()} \t-\t {Request.Path} \t-\t {res.ProcessingTimeInMs} ms");
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Utils.GetNow()} \t-\t Request.Path \t-\t {ex.Message}");
                return BadRequest(new ErrorResponse(ex));
            }
        }

        [HttpPost("encrypt")]
        [Consumes("application/json")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        [ProducesResponseType(typeof(EncryptionResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public ActionResult<EncryptionResponse> Encrypt([FromBody] EncryptionRequest encRequest)
        {
            try
            {
                _logger.LogInformation($"{Utils.GetNow()} \t-\t {Request.Path} \t-\t {Request.ContentLength} bytes");
                EncryptionResponse res = EncryptionRequestHandler.Handle(encRequest);
                _logger.LogInformation($"{Utils.GetNow()} \t-\t {Request.Path} \t-\t {res.ProcessingTimeInMs} ms");
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Utils.GetNow()} \t-\t Request.Path \t-\t {ex.Message}");
                return BadRequest(new ErrorResponse(ex));
            }
        }

        [HttpPost("decrypt")]
        [Consumes("application/json")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        [ProducesResponseType(typeof(DecryptionResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public ActionResult<DecryptionResponse> Decrypt([FromBody] DecryptionRequest decRequest)
        {
            try
            {
                _logger.LogInformation($"{Utils.GetNow()} \t-\t {Request.Path} \t-\t {Request.ContentLength} bytes");
                DecryptionResponse res = DecryptionRequestHandler.Handle(decRequest);
                _logger.LogInformation($"{Utils.GetNow()} \t-\t {Request.Path} \t-\t {res.ProcessingTimeInMs} ms");
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Utils.GetNow()} \t-\t Request.Path \t-\t {ex.Message}");
                return BadRequest(new ErrorResponse(ex));
            }
        }

    }
}
