using CAAS.Handlers.Rng;
using CAAS.Models.Rng;
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
    public class RngController : ControllerBase
    {
        private readonly ILogger<RngController> _logger;

        public RngController(ILogger<RngController> logger)
        {
            _logger = logger;
        }

        [HttpPost("generate")]
        [Consumes("application/json")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        [ProducesResponseType(typeof(RngResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public ActionResult<RngResponse> Generate([FromBody] RngRequest RngRequest)
        {
            try
            {
                _logger.LogInformation($"{Utils.GetNow()} \t-\t {Request.Path} \t-\t {Request.ContentLength} bytes");
                RngResponse res = RngRequestHandler.Handle(RngRequest);
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
                //_logger.LogInformation($"{Utils.GetNow()} \t-\t {Request.Path} \t-\t {res.ProcessingTimeInMs} ms");
                return Ok(RngSupportedAlgorithmsValues.values);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Utils.GetNow()} \t-\t Request.Path \t-\t {ex.Message}");
                return BadRequest(new ErrorResponse(ex));
            }
        }

    }
}
