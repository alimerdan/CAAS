using CAAS.Exceptions;
using CAAS.Models.Rng;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.ComponentModel;

namespace CAAS.Tests.Controllers
{
    public class RngControllerTests
    {


        [Theory]
        [InlineData("csprng", 1, "hex")]
        [Description("Test Generate API response is valid HTTP response")]
        public void TestGenerateAPIResponseIsValidHTTP(string _algorithm, int _size, string _outputDataFormat)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.RngController>>();
            CAAS.Controllers.RngController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            RngRequest req = new()
            {
                Algorithm = _algorithm,
                Size = _size,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<RngResponse> res = controller.Generate(req);
            Assert.IsType<OkObjectResult>(res.Result);

        }

        [Theory]
        [InlineData("csprng", 1, "hex")]
        [InlineData("csprng", 0, "hex")]
        [InlineData("csprng", 16, "hex")]
        [InlineData("csprng", 20, "hex")]
        [Description("Test Generate API response values")]
        public void TestGenerateAPIResponseValues(string _algorithm, int _size, string _outputDataFormat)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.RngController>>();
            CAAS.Controllers.RngController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            RngRequest req = new()
            {
                Algorithm = _algorithm,
                Size = _size,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<RngResponse> res = controller.Generate(req);
            Assert.IsType<OkObjectResult>(res.Result);
            RngResponse? responseObject = (res.Result as ObjectResult).Value as RngResponse;
            Assert.True(responseObject.Rng.Length / 2 == _size);
            Assert.True(responseObject.ProcessingTimeInMs >= 0);

        }

        [Theory]
        [InlineData("csprng", 1, "hex")]
        [Description("Test Generate API BadRequest reponse, if HttpContext is missing")]
        public void TestGenerateBadRequestResponse(string _algorithm, int _size, string _outputDataFormat)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.RngController>>();
            CAAS.Controllers.RngController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            RngRequest req = new()
            {
                Algorithm = _algorithm,
                Size = _size,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<RngResponse> res = controller.Generate(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
        }

        [Theory]
        [InlineData("csprng", 1, "xxx", typeof(NotSupportedDataFormatException))]
        [InlineData("csprng", 1, "", typeof(NotSupportedDataFormatException))]
        [Description("Test Generate API NotSupportedFormat response")]
        public void TestGenerateNotSupportedDataFormatResponse(string _algorithm, int _size, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.RngController>>();
            CAAS.Controllers.RngController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            RngRequest req = new()
            {
                Algorithm = _algorithm,
                Size = _size,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<RngResponse> res = controller.Generate(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);

        }

        [Theory]
        [InlineData("xxx", 1, "hex", typeof(NotSupportedAlgorithmException))]
        [InlineData("", 1, "hex", typeof(NotSupportedAlgorithmException))]
        [InlineData("aes_cbc_pkcs7", 1, "hex", typeof(NotSupportedAlgorithmException))]
        [Description("Test Generate API NotSupportedAlgorithm response")]
        public void TestInvalidGenerateNotSupportedAlgorithmResponse(string _algorithm, int _size, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.RngController>>();
            CAAS.Controllers.RngController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            RngRequest req = new()
            {
                Algorithm = _algorithm,
                Size = _size,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<RngResponse> res = controller.Generate(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);

        }

        [Theory]
        [InlineData("csprng", -9, "hex", typeof(OverflowException))]
        [Description("Test Generate API Overflow response")]
        public void TestGenerateOverFlowResponse(string _algorithm, int _size, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.RngController>>();
            CAAS.Controllers.RngController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            RngRequest req = new()
            {
                Algorithm = _algorithm,
                Size = _size,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<RngResponse> res = controller.Generate(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);

        }

        [Fact]
        [Description("Test GetSupportedAlgorithms API response is valid HTTP response")]
        public void TestSupportedAlgorithmsIsValidHTTP()
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.RngController>>();
            CAAS.Controllers.RngController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            ActionResult<HashSet<string>> res = controller.GetSupportedAlgorithms();
            Assert.IsType<OkObjectResult>(res.Result);

        }

        [Fact]
        [Description("Test GetSupportedAlgorithms API response values")]
        public void TestSupportedAlgorithmsAPIResposeValue()
        {
            HashSet<string> expectedResult = new()
        {
            {"csprng" }
        };
            var logger = Mock.Of<ILogger<CAAS.Controllers.RngController>>();
            CAAS.Controllers.RngController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            ActionResult<HashSet<string>> res = controller.GetSupportedAlgorithms();
            Assert.IsType<OkObjectResult>(res.Result);
            HashSet<string>? responseObject = (res.Result as ObjectResult).Value as HashSet<string>;
            Assert.Equal(responseObject, expectedResult);
        }


        [Fact]
        [Description("Test GetSupportedAlgorithms API BadRequest response, if httpContext is missing")]
        public void TestSupportedAlgorithmsBadRequestResponse()
        {

            var logger = Mock.Of<ILogger<CAAS.Controllers.RngController>>();
            CAAS.Controllers.RngController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            ActionResult<HashSet<string>> res = controller.GetSupportedAlgorithms();
            Assert.IsType<BadRequestObjectResult>(res.Result as BadRequestObjectResult);

        }

    }
}
