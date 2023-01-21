using CAAS.Exceptions;
using CAAS.Models.Hash;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CAAS.Tests.Controllers
{
    public class HashControllerTests
    {
        [Theory]
        [InlineData("sha256", "0011223344556677", "hex", "hex", "D1A5F998FA6ED82DA6943127533B412F2286B30C8473A819F70A8FEC5913FEA7")]
        public void DigestTests(string _algorithm, string _data, string _inputDataFormat, string _outputDataFormat, string _expectedResult)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.HashController>>();
            CAAS.Controllers.HashController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            HashRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<HashResponse> res = controller.Digest(req);
            Assert.NotNull(res);
            Assert.NotNull(res.Result as OkObjectResult);
            HashResponse? responseObject = (res.Result as ObjectResult).Value as HashResponse;
            Assert.NotNull(responseObject);
            Assert.Equal(_expectedResult, responseObject.Digest);
            Assert.True(responseObject.ProcessingTimeInMs >= 0);

        }
        [Theory]
        [InlineData("sha256", "0011223344556677", "byx", "hex", typeof(NotSupportedDataFormatException))]
        [InlineData("sha11256", "0011223344556677", "hex", "hex", typeof(NotSupportedAlgorithmException))]
        public void InvalidDigestTests(string _algorithm, string _data, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.HashController>>();
            CAAS.Controllers.HashController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            HashRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<HashResponse> res = controller.Digest(req);
            Assert.NotNull(res);
            Assert.NotNull(res.Result as BadRequestObjectResult);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.NotNull(responseObject);
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);

        }

        [Fact]
        public void GetAlgorithmTests()
        {
            HashSet<string> expectedResult = new()
        {
            {"sha256" }
        };
            var logger = Mock.Of<ILogger<CAAS.Controllers.HashController>>();
            CAAS.Controllers.HashController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            ActionResult<HashSet<string>> res = controller.GetSupportedAlgorithms();
            Assert.NotNull(res);
            Assert.NotNull(res.Result as OkObjectResult);
            HashSet<string>? responseObject = (res.Result as ObjectResult).Value as HashSet<string>;
            Assert.NotNull(responseObject);
            Assert.Equal(responseObject, expectedResult);

        }

        [Fact]
        public void InvalidGetAlgorithmTests()
        {

            var logger = Mock.Of<ILogger<CAAS.Controllers.HashController>>();
            CAAS.Controllers.HashController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            ActionResult<HashSet<string>> res = controller.GetSupportedAlgorithms();
            Assert.NotNull(res);
            Assert.NotNull(res.Result as BadRequestObjectResult);

        }

    }
}
