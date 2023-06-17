using CAAS.Exceptions;
using CAAS.Models.Mac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.ComponentModel;

namespace CAAS.Tests.Controllers
{
    public class MacControllerTests
    {


        [Theory]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "hex", "hex")]
        [InlineData("sha256_hmac", "0011223344556677", "00112233445566770011223344556677", "hex", "hex")]
        [Description("Test Generate API returns valid HTTP object")]
        public void TestGenerateAPIResponseIsValidHTTP(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            MacRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<MacResponse> res = controller.Generate(req);
            Assert.IsType<OkObjectResult>(res.Result);
        }

        [Theory]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", "F8409911928AEBF52A0C3A88AABE16A6")]
        [InlineData("sha256_hmac", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", "D179466C31155FE42B42D130C9794765B217CA22983F294F478137A60CAA29C1")]
        [Description("Test Generate API response value")]
        public void TestGenerateAPIResponseValue(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, string _expectedResult)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            MacRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<MacResponse> res = controller.Generate(req);
            Assert.IsType<OkObjectResult>(res.Result);
            MacResponse? responseObject = (res.Result as ObjectResult).Value as MacResponse;
            Assert.Equal(_expectedResult, responseObject.Mac);
            Assert.True(responseObject.ProcessingTimeInMs >= 0);
        }

        [Theory]
        [InlineData("aes128_cmac", "0011223344556677", "0011223344556677", "hex", "hex", typeof(Exception))]
        [Description("Test Generate API response if invalid key provided")]
        public void TestGenerateInvalidKeyLengthException(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            MacRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<MacResponse> res = controller.Generate(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);
        }

        [Theory]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "xxxx", "hex", typeof(NotSupportedDataFormatException))]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "", "hex", typeof(NotSupportedDataFormatException))]
        [InlineData("sha256_hmac", "0011223344556677", "00112233445566770011223344556677", "hex", "xxxx", typeof(NotSupportedDataFormatException))]
        [InlineData("sha256_hmac", "0011223344556677", "00112233445566770011223344556677", "hex", "", typeof(NotSupportedDataFormatException))]
        [InlineData("sha256_hmac", "0011223344556677", "00112233445566770011223344556677", "xxx", "xxx", typeof(NotSupportedDataFormatException))]
        [InlineData("sha256_hmac", "0011223344556677", "00112233445566770011223344556677", "", "", typeof(NotSupportedDataFormatException))]
        [Description("Test Generate API response if not supported data format provided")]
        public void TestGenerateNotsupportedDataFormatException(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            MacRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<MacResponse> res = controller.Generate(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);
        }

        [Theory]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "hex", "ascii", typeof(NotSupportedDataFormatForOperationException))]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "hex", "utf8", typeof(NotSupportedDataFormatForOperationException))]
        [InlineData("sha256_hmac", "0011223344556677", "00112233445566770011223344556677", "hex", "ascii", typeof(NotSupportedDataFormatForOperationException))]
        [InlineData("sha256_hmac", "0011223344556677", "00112233445566770011223344556677", "hex", "utf8", typeof(NotSupportedDataFormatForOperationException))]
        [Description("Test Generate API response if not supported data format for operation")]
        public void TestGenerateNotsupportedDataFormatForOperationExceptionn(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            MacRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<MacResponse> res = controller.Generate(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);
        }


        [Theory]
        [InlineData("xxxx", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", typeof(NotSupportedAlgorithmException))]
        [InlineData("", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", typeof(NotSupportedAlgorithmException))]
        [InlineData("aes_cbc_pkcs7", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", typeof(NotSupportedAlgorithmException))]
        [Description("Test Generate API response if not supported algorithm provided")]
        public void TestGenerateNotsupportedAlgorithmException(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            MacRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<MacResponse> res = controller.Generate(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);
        }

        [Fact]
        [Description("Test GetSupportedAlgorithms API response is a valid HTTP response")]
        public void TestSupportedAlgorithmsIsValidHTTP()
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            ActionResult<HashSet<string>> res = controller.GetSupportedAlgorithms();
            Assert.IsType<OkObjectResult>(res.Result);

        }

        [Fact]
        [Description("Test GetSupportedAlgorithms API response values")]
        public void TestSupportedAlgorithmsValues()
        {
            HashSet<string> expectedResult = new()
        {
            {"aes128_cmac"},{"sha256_hmac"}
        };
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            ActionResult<HashSet<string>> res = controller.GetSupportedAlgorithms();
            HashSet<string>? responseObject = (res.Result as ObjectResult).Value as HashSet<string>;
            Assert.Equal(responseObject, expectedResult);

        }

        [Fact]
        [Description("Test GetSupportedAlgorithms API response is a BadRequest when http conext is not provided")]
        public void TestSupportedAlgorithmBadRequestResponse()
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            ActionResult<HashSet<string>> res = controller.GetSupportedAlgorithms();
            Assert.IsType<BadRequestObjectResult>(res.Result as BadRequestObjectResult);

        }
    }
}
