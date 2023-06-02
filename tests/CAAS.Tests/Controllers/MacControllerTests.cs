using CAAS.Exceptions;
using CAAS.Models.Mac.Sign;
using CAAS.Models.Mac.Verify;
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
        [Description("Test Sign API returns valid HTTP object")]
        public void TestSignAPIResponseIsValidHTTP(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SignRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SignResponse> res = controller.Sign(req);
            Assert.IsType<OkObjectResult>(res.Result);
        }

        [Theory]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", "F8409911928AEBF52A0C3A88AABE16A6")]
        [InlineData("sha256_hmac", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", "D179466C31155FE42B42D130C9794765B217CA22983F294F478137A60CAA29C1")]
        [Description("Test Sign API response value")]
        public void TestSignAPIResponseValue(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, string _expectedResult)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SignRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SignResponse> res = controller.Sign(req);
            Assert.IsType<OkObjectResult>(res.Result);
            SignResponse? responseObject = (res.Result as ObjectResult).Value as SignResponse;
            Assert.Equal(_expectedResult, responseObject.Mac);
            Assert.True(responseObject.ProcessingTimeInMs >= 0);
        }

        [Theory]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "F8409911928AEBF52A0C3A88AABE16A6", "hex")]
        [InlineData("sha256_hmac", "0011223344556677", "00112233445566770011223344556677", "D179466C31155FE42B42D130C9794765B217CA22983F294F478137A60CAA29C1", "hex")]
        [Description("Test Verify API returns valid HTTP object")]
        public void TestVerifyAPIResponseIsValidHTTP(string _algorithm, string _data, string _key, string _signature, string _inputDataFormat)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            VerifyRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                Signature = _signature,
                InputDataFormat = _inputDataFormat

            };
            ActionResult<VerifyResponse> res = controller.Verify(req);
            Assert.IsType<OkObjectResult>(res.Result);
        }

        [Theory]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "F8409911928AEBF52A0C3A88AABE16A6", "hex", true)]
        [InlineData("sha256_hmac", "0011223344556677", "00112233445566770011223344556677", "D179466C31155FE42B42D130C9794765B217CA22983F294F478137A60CAA29C1", "hex", true)]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "0000", "hex", false)]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "", "hex", false)]
        [InlineData("sha256_hmac", "0011223344556677", "00112233445566770011223344556677", "0000", "hex", false)]
        [InlineData("sha256_hmac", "0011223344556677", "00112233445566770011223344556677", "", "hex", false)]
        [Description("Test Verify API returns valid HTTP object")]
        public void TestVerifyAPIResponseValue(string _algorithm, string _data, string _key, string _signature, string _inputDataFormat, bool _isVerified)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            VerifyRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                Signature = _signature,
                InputDataFormat = _inputDataFormat

            };
            ActionResult<VerifyResponse> res = controller.Verify(req);
            Assert.IsType<OkObjectResult>(res.Result);
            VerifyResponse? verifyResponseObject = (res.Result as ObjectResult).Value as VerifyResponse;
            Assert.Equal(verifyResponseObject.IsVerified, _isVerified);
            Assert.True(verifyResponseObject.ProcessingTimeInMs >= 0);
        }


        [Theory]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", "F8409911928AEBF52A0C3A88AABE16A6")]
        [InlineData("sha256_hmac", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", "D179466C31155FE42B42D130C9794765B217CA22983F294F478137A60CAA29C1")]
        [Description("Test Sign and Verify APIs full cycle")]
        public void TestSignVerifyFullCycle(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, string _expectedResult)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SignRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SignResponse> res = controller.Sign(req);
            Assert.IsType<OkObjectResult>(res.Result);
            SignResponse? responseObject = (res.Result as ObjectResult).Value as SignResponse;
            Assert.Equal(_expectedResult, responseObject.Mac);
            Assert.True(responseObject.ProcessingTimeInMs >= 0);

            VerifyRequest verifyReq = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                Signature = responseObject.Mac
            };
            ActionResult<VerifyResponse> verifyRes = controller.Verify(verifyReq);
            Assert.IsType<OkObjectResult>(verifyRes.Result);
            VerifyResponse? verifyResponseObject = (verifyRes.Result as ObjectResult).Value as VerifyResponse;
            Assert.True(verifyResponseObject.IsVerified);
            Assert.True(verifyResponseObject.ProcessingTimeInMs >= 0);
        }



        [Theory]
        [InlineData("aes128_cmac", "0011223344556677", "0011223344556677", "hex", "hex", typeof(Exception))]
        [Description("Test Sign API response if invalid key provided")]
        public void TestignInvalidKeyLengthException(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SignRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SignResponse> res = controller.Sign(req);
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
        [Description("Test Sign API response if not supported data format provided")]
        public void TestSignNotsupportedDataFormatException(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SignRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SignResponse> res = controller.Sign(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);
        }

        [Theory]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "hex", "ascii", typeof(NotSupportedDataFormatForOperationException))]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "hex", "utf8", typeof(NotSupportedDataFormatForOperationException))]
        [InlineData("sha256_hmac", "0011223344556677", "00112233445566770011223344556677", "hex", "ascii", typeof(NotSupportedDataFormatForOperationException))]
        [InlineData("sha256_hmac", "0011223344556677", "00112233445566770011223344556677", "hex", "utf8", typeof(NotSupportedDataFormatForOperationException))]
        [Description("Test Sign API response if not supported data format for operation")]
        public void TestSignNotsupportedDataFormatForOperationExceptionn(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SignRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SignResponse> res = controller.Sign(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);
        }


        [Theory]
        [InlineData("xxxx", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", typeof(NotSupportedAlgorithmException))]
        [InlineData("", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", typeof(NotSupportedAlgorithmException))]
        [InlineData("aes_cbc_pkcs7", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", typeof(NotSupportedAlgorithmException))]
        [Description("Test Sign API response if not supported algorithm provided")]
        public void TestSignNotsupportedAlgorithmException(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SignRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SignResponse> res = controller.Sign(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);
        }

        [Theory]
        [InlineData("aes128_cmac", "0011223344556677", "0011223344556677", "F8409911928AEBF52A0C3A88AABE16A6", "hex", typeof(Exception))]
        [Description("Test Verify API response if invalid key provided")]
        public void TestVerifyInvalidKeyLengthException(string _algorithm, string _data, string _key, string _signature, string _inputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            VerifyRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Signature = _signature,
                Key = _key,
                InputDataFormat = _inputDataFormat,

            };
            ActionResult<VerifyResponse> res = controller.Verify(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);
        }

        [Theory]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "F8409911928AEBF52A0C3A88AABE16A6", "xxxx", typeof(NotSupportedDataFormatException))]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "F8409911928AEBF52A0C3A88AABE16A6", "", typeof(NotSupportedDataFormatException))]
        [Description("Test Verify API response if not supported data format provided")]
        public void TestVerifyNotsupportedDataFormatException(string _algorithm, string _data, string _key, string _signature, string _inputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            VerifyRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                Signature = _signature,
                InputDataFormat = _inputDataFormat

            };
            ActionResult<VerifyResponse> res = controller.Verify(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);
        }

        [Theory]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "F8409911928AEBF52A0C3A88AABE16A6", "ascii", typeof(NotSupportedDataFormatForOperationException))]
        [InlineData("aes128_cmac", "0011223344556677", "00112233445566770011223344556677", "F8409911928AEBF52A0C3A88AABE16A6", "utf8", typeof(NotSupportedDataFormatForOperationException))]
        [Description("Test Verify API response if not supported data format for operation")]
        public void TestVerifyNotsupportedDataFormatForOperationException(string _algorithm, string _data, string _key, string _signature, string _inputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            VerifyRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                Signature = _signature,
                InputDataFormat = _inputDataFormat

            };
            ActionResult<VerifyResponse> res = controller.Verify(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);
        }

        [Theory]
        [InlineData("xxxx", "0011223344556677", "00112233445566770011223344556677", "F8409911928AEBF52A0C3A88AABE16A6", "hex", typeof(NotSupportedAlgorithmException))]
        [InlineData("", "0011223344556677", "00112233445566770011223344556677", "F8409911928AEBF52A0C3A88AABE16A6", "hex", typeof(NotSupportedAlgorithmException))]
        [InlineData("aes_cbc_pkcs7", "0011223344556677", "00112233445566770011223344556677", "F8409911928AEBF52A0C3A88AABE16A6", "hex", typeof(NotSupportedAlgorithmException))]
        [Description("Test Verify API response if not supported algorithm provided")]
        public void TestVerifyNotsupportedAlgorithmException(string _algorithm, string _data, string _key, string _signature, string _inputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.MacController>>();
            CAAS.Controllers.MacController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            VerifyRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                Signature = _signature,
                InputDataFormat = _inputDataFormat

            };
            ActionResult<VerifyResponse> res = controller.Verify(req);
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
