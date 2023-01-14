using CAAS.Handlers.Hash;
using CAAS.Models;
using CAAS.Models.Hash;
using CAAS.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CAAS.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class HashController : ControllerBase
    {
        private readonly ILogger<HashController> _logger;

        public HashController(ILogger<HashController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Consumes("application/json")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        [ProducesResponseType(typeof(Models.Hash.HashResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public ActionResult<HashResponse> Encrypt([FromBody] HashRequest hashRequest, [FromHeader(Name = "CAAS-Data-Type")] CAASDataType dataType=CAASDataType.hex)
        {
            try
            {
                _logger.LogInformation($"{Utils.GetNow()} \t-\t {Request.Path} \t-\t {Request.ContentLength} bytes");
                CAASDataType retDataType;
                HashResponse res = HashRequestHandler.Handle(hashRequest,dataType, out retDataType);
                Response.Headers.Add("CAAS-Data-Type", retDataType.ToString());
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
