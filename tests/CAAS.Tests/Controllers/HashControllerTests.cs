using CAAS.Exceptions;
using CAAS.Models.Hash;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.ComponentModel;

namespace CAAS.Tests.Controllers
{
    public class HashControllerTests
    {
        [Theory]
        [InlineData("sha256", "0011223344556677", "hex", "hex")]
        [Description("Test Digest API returns a valid HTTP response")]
        public void TestDigestIsValidHTTPResonse(string _algorithm, string _data, string _inputDataFormat, string _outputDataFormat)
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
            Assert.IsType<OkObjectResult>(res.Result);

        }

        [Theory]
        [InlineData("sha256", "0011223344556677", "hex", "hex")]
        [Description("Test Digest API returns valid structure")]
        public void TestDigestAPIResponseStructure(string _algorithm, string _data, string _inputDataFormat, string _outputDataFormat)
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
            Assert.IsType<OkObjectResult>(res.Result);
            Assert.IsType<HashResponse>((res.Result as ObjectResult).Value);

        }

        [Theory]
        [InlineData("sha256", "0011223344556677", "hex", "hex", "D1A5F998FA6ED82DA6943127533B412F2286B30C8473A819F70A8FEC5913FEA7")]
        [Description("Test Digest API returns valid value")]
        public void TestDigestAPIResponseValue(string _algorithm, string _data, string _inputDataFormat, string _outputDataFormat, string _expectedResult)
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
            Assert.IsType<OkObjectResult>(res.Result);
            HashResponse? responseObject = (res.Result as ObjectResult).Value as HashResponse;
            Assert.Equal(_expectedResult, responseObject.Digest);
            Assert.True(responseObject.ProcessingTimeInMs >= 0);
        }

        [Theory]
        [InlineData("sha256", "0011223344556677", "byx", "hex")]
        [Description("Test Digest API returns BadRequest Response")]
        public void TestInvalidDigestBadRequestResponse(string _algorithm, string _data, string _inputDataFormat, string _outputDataFormat)
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
            Assert.IsType<BadRequestObjectResult>(res.Result);

        }

        [Theory]
        [InlineData("sha256", "0011223344556677", "byx", "hex", typeof(NotSupportedDataFormatException))]
        [InlineData("sha256", "0011223344556677", "", "hex", typeof(NotSupportedDataFormatException))]
        [InlineData("sha256", "0011223344556677", "hex", "byz", typeof(NotSupportedDataFormatException))]
        [InlineData("sha256", "0011223344556677", "hex", "", typeof(NotSupportedDataFormatException))]
        [InlineData("sha256", "0011223344556677", "aaa", "bbb", typeof(NotSupportedDataFormatException))]
        [InlineData("sha256", "0011223344556677", "", "", typeof(NotSupportedDataFormatException))]
        [Description("Test Digest API returns NotSupportedDataFormat Response")]
        public void TestInvalidDigestNotSupportedDataFormatExceptionResponse(string _algorithm, string _data, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
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
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);

        }

        [Theory]
        [InlineData("sha256", "0011223344556677", "hex", "utf8", typeof(NotSupportedDataFormatForOperationException))]
        [InlineData("sha256", "0011223344556677", "hex", "ascii", typeof(NotSupportedDataFormatForOperationException))]
        [Description("Test Digest API returns NotSupportedDataFormatForOperation Response")]
        public void TestInvalidDigestNotSupportedDataFormatForOperationExceptionResponse(string _algorithm, string _data, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
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
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);

        }

        [Theory]
        [InlineData("sha11256", "0011223344556677", "hex", "hex", typeof(NotSupportedAlgorithmException))]
        [InlineData("zzz", "0011223344556677", "hex", "hex", typeof(NotSupportedAlgorithmException))]
        [InlineData("", "0011223344556677", "hex", "hex", typeof(NotSupportedAlgorithmException))]
        [Description("Test Digest API returns NotSupportedAlgorithm Response")]
        public void TestInvalidDigestNotSupportedAlgorithmExceptionResponse(string _algorithm, string _data, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
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
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);

        }

        [Fact]
        [Description("Test Digest GetAlgorithms API Response is valid HTTP response")]
        public void TestDigestAlgorithmsIsValidHTTPResponse()
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.HashController>>();
            CAAS.Controllers.HashController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            ActionResult<HashSet<string>> res = controller.GetSupportedAlgorithms();
            Assert.IsType<OkObjectResult>(res.Result);


        }

        [Fact]
        [Description("Test Digest GetAlgorithms API Response Values")]
        public void TestDigestAlgorithmsResponseValues()
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
            Assert.IsType<OkObjectResult>(res.Result);
            HashSet<string>? responseObject = (res.Result as ObjectResult).Value as HashSet<string>;
            Assert.Equal(responseObject, expectedResult);
        }


        [Fact]
        [Description("Test Invalid Digest GetAlgorithms API BadRequest Response, when httpConext is not sent")]
        public void TestInvalidGetAlgorithmsBadRequestResponse()
        {

            var logger = Mock.Of<ILogger<CAAS.Controllers.HashController>>();
            CAAS.Controllers.HashController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            ActionResult<HashSet<string>> res = controller.GetSupportedAlgorithms();
            Assert.IsType<BadRequestObjectResult>(res.Result);

        }

    }
}
