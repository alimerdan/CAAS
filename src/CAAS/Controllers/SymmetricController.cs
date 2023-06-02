
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
    public class SymmetricController : ControllerBase
    {
        private readonly ILogger<SymmetricController> _logger;

        public SymmetricController(ILogger<SymmetricController> logger)
        {
            _logger = logger;
        }

        [HttpPost("encrypt")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(SymmetricEncryptionResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public ActionResult<SymmetricEncryptionResponse> Encrypt([FromBody] SymmetricEncryptionRequest encRequest)
        {
            try
            {
                _logger.LogInformation($"{Utils.GetNow()} \t-\t {Request.Path} \t-\t {Request.ContentLength} bytes");
                SymmetricEncryptionResponse res = SymmetricEncryptionRequestHandler.Handle(encRequest);
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
        [ProducesResponseType(typeof(SymmetricDecryptionResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public ActionResult<SymmetricDecryptionResponse> Decrypt([FromBody] SymmetricDecryptionRequest decRequest)
        {
            try
            {
                _logger.LogInformation($"{Utils.GetNow()} \t-\t {Request.Path} \t-\t {Request.ContentLength} bytes");
                SymmetricDecryptionResponse res = SymmetricDecryptionRequestHandler.Handle(decRequest);
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
        public ActionResult<HashSet<string>> GetSupportedAlgorithms()
        {
            try
            {
                _logger.LogInformation($"{Utils.GetNow()} \t-\t {Request.Path} \t-\t {Request.ContentLength} bytes");
                //TODO: Add processingtime in ms
                return Ok(SymmetricSupportedAlgorithmsValues.Values);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Utils.GetNow()} \t-\t Request.Path \t-\t {ex.Message}");
                return BadRequest(new ErrorResponse(ex));
            }
        }

    }
}
