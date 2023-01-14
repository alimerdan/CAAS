
using CAAS.Handlers.Symmetric;
using CAAS.Models.Symmetric;
using CAAS.Models.Symmetric.Decryption;
using CAAS.Models.Symmetric.Encryption;
using CAAS.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CAAS.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    //[Route("api/v1/")]
    public class SymmetricController : ControllerBase
    {
        private readonly ILogger<SymmetricController> _logger;

        public SymmetricController(ILogger<SymmetricController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Encrypt")]
        [Consumes("application/json")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        [ProducesResponseType(typeof(EncryptionResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
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

        /// <summary>
        /// Decrypt Data
        /// </summary>
        /// <param name="decRequest"></param>
        /// <returns>Response Object</returns>
        [HttpPost("Decrypt")]
        [Consumes("application/json")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        [ProducesResponseType(typeof(DecryptionResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
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

        [HttpGet("algorithms")]
        [ProducesResponseType(typeof(HashSet<string>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public ActionResult<HashSet<string>> GetHashAlgorithms()
        {
            try
            {
                _logger.LogInformation($"{Utils.GetNow()} \t-\t {Request.Path} \t-\t {Request.ContentLength} bytes");
                //_logger.LogInformation($"{Utils.GetNow()} \t-\t {Request.Path} \t-\t {res.ProcessingTimeInMs} ms");
                return Ok(SymmetricSupportedAlgorithmsValues.values);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Utils.GetNow()} \t-\t Request.Path \t-\t {ex.Message}");
                return BadRequest(new ErrorResponse(ex));
            }
        }

    }
}
