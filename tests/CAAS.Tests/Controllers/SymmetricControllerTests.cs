using CAAS.CryptoLib.Exceptions;
using CAAS.Exceptions;
using CAAS.Models.Symmetric.Decryption;
using CAAS.Models.Symmetric.Encryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.ComponentModel;

namespace CAAS.Tests.Controllers
{
    public class SymmetricControllerTests
    {
        [Theory]
        [InlineData("aes_cbc_pkcs7", "0011223344556677", "00112233445566770011223344556677", "hex", "hex")]
        [InlineData("aes_ecb_pkcs7", "0011223344556677", "00112233445566770011223344556677", "hex", "hex")]
        [Description("Test Encrypt API returns a valid HTTP response")]
        public void TestEncryptResponseIsValidHTTP(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SymmetricEncryptionRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SymmetricEncryptionResponse> res = controller.Encrypt(req);
            Assert.IsType<OkObjectResult>(res.Result as OkObjectResult);
        }

        [Theory]
        [InlineData("aes_cbc_pkcs7", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", "C656C652E6656125139C219FD9F6EABB")]
        [InlineData("aes_ecb_pkcs7", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", "C656C652E6656125139C219FD9F6EABB")]
        [Description("Test Encrypt API response values")]
        public void TestEncryptResponseValues(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, string _expectedResult)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SymmetricEncryptionRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SymmetricEncryptionResponse> res = controller.Encrypt(req);
            Assert.IsType<OkObjectResult>(res.Result);
            SymmetricEncryptionResponse? responseObject = (res.Result as ObjectResult).Value as SymmetricEncryptionResponse;
            Assert.Equal(_expectedResult, responseObject.CipherData);
            Assert.True(responseObject.ProcessingTimeInMs >= 0);

        }

        [Theory]
        [InlineData("aes_cbc_pkcs7", "0011223344556677", "0011223344556677", "hex", "hex", typeof(CaaSCryptoException))]
        [InlineData("aes_ecb_pkcs7", "0011223344556677", "0011223344556677", "hex", "hex", typeof(CaaSCryptoException))]
        [Description("Test Encrypt API invalid key length response")]
        public void TestEncryptInvalidKeyLengthResponse(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SymmetricEncryptionRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SymmetricEncryptionResponse> res = controller.Encrypt(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);
        }

        [Theory]
        [InlineData("aes_ecb_pkcs7", "0011223344556677", "00112233445566770011223344556677", "xxxx", "hex", typeof(NotSupportedDataFormatException))]
        [InlineData("aes_ecb_pkcs7", "0011223344556677", "00112233445566770011223344556677", "hex", "xxxx", typeof(NotSupportedDataFormatException))]
        [InlineData("aes_ecb_pkcs7", "0011223344556677", "00112233445566770011223344556677", "", "hex", typeof(NotSupportedDataFormatException))]
        [InlineData("aes_ecb_pkcs7", "0011223344556677", "00112233445566770011223344556677", "hex", "", typeof(NotSupportedDataFormatException))]
        [InlineData("aes_ecb_pkcs7", "0011223344556677", "00112233445566770011223344556677", "", "", typeof(NotSupportedDataFormatException))]
        [InlineData("aes_ecb_pkcs7", "0011223344556677", "00112233445566770011223344556677", "xxx", "xxx", typeof(NotSupportedDataFormatException))]
        [Description("Test Encrypt API not supported data format response")]
        public void TestEncryptNotSupportedDataFormatResponse(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SymmetricEncryptionRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SymmetricEncryptionResponse> res = controller.Encrypt(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);

        }

        [Theory]
        [InlineData("xxxx", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", typeof(NotSupportedAlgorithmException))]
        [InlineData("", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", typeof(NotSupportedAlgorithmException))]
        [Description("Test Encrypt API not supported algorithm response")]
        public void TestEncryptNotSupportedAlgorithmResponse(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SymmetricEncryptionRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SymmetricEncryptionResponse> res = controller.Encrypt(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);

        }


        [Theory]
        [InlineData("aes_cbc_pkcs7", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "hex", "hex")]
        [InlineData("aes_ecb_pkcs7", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "hex", "hex")]
        [Description("Test Decrypt API returns a valid HTTP response")]
        public void TestDecryptResponseIsValidHTTP(string _algorithm, string _cipherData, string _key, string _inputDataFormat, string _outputDataFormat)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SymmetricDecryptionRequest req = new()
            {
                Algorithm = _algorithm,
                CipherData = _cipherData,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SymmetricDecryptionResponse> res = controller.Decrypt(req);
            Assert.IsType<OkObjectResult>(res.Result);

        }

        [Theory]
        [InlineData("aes_cbc_pkcs7", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "hex", "hex", "0011223344556677")]
        [InlineData("aes_ecb_pkcs7", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "hex", "hex", "0011223344556677")]
        [Description("Test Decrypt API response values")]
        public void TestDecryptResponseValues(string _algorithm, string _cipherData, string _key, string _inputDataFormat, string _outputDataFormat, string _expectedResult)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SymmetricDecryptionRequest req = new()
            {
                Algorithm = _algorithm,
                CipherData = _cipherData,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SymmetricDecryptionResponse> res = controller.Decrypt(req);
            Assert.IsType<OkObjectResult>(res.Result);
            SymmetricDecryptionResponse? responseObject = (res.Result as ObjectResult).Value as SymmetricDecryptionResponse;
            Assert.Equal(_expectedResult, responseObject.Data);
            Assert.True(responseObject.ProcessingTimeInMs >= 0);

        }


        [Theory]
        [InlineData("aes_cbc_pkcs7", "C656C652E6656125139C219FD9F6EABB", "0011223344556677", "hex", "hex", typeof(CaaSCryptoException))]
        [InlineData("aes_ecb_pkcs7", "C656C652E6656125139C219FD9F6EABB", "0011223344556677", "hex", "hex", typeof(CaaSCryptoException))]
        [Description("Test Decrypt API invalid key length response")]
        public void TestDecryptionInvalidKeyLengthResponse(string _algorithm, string _cipherData, string _key, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SymmetricDecryptionRequest req = new()
            {
                Algorithm = _algorithm,
                CipherData = _cipherData,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SymmetricDecryptionResponse> res = controller.Decrypt(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);

        }

        [Theory]

        [InlineData("aes_cbc_pkcs7", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "hex", "xxxx", typeof(NotSupportedDataFormatException))]
        [InlineData("aes_cbc_pkcs7", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "xxxx", "hex", typeof(NotSupportedDataFormatException))]
        [InlineData("aes_cbc_pkcs7", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "hex", "", typeof(NotSupportedDataFormatException))]
        [InlineData("aes_cbc_pkcs7", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "", "hex", typeof(NotSupportedDataFormatException))]
        [InlineData("aes_cbc_pkcs7", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "xxx", "xxxx", typeof(NotSupportedDataFormatException))]
        [InlineData("aes_cbc_pkcs7", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "", "", typeof(NotSupportedDataFormatException))]
        [Description("Test Decrypt API not supported data format response")]
        public void TestDecryptionNotSupportedDataFormatResponse(string _algorithm, string _cipherData, string _key, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SymmetricDecryptionRequest req = new()
            {
                Algorithm = _algorithm,
                CipherData = _cipherData,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SymmetricDecryptionResponse> res = controller.Decrypt(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);

        }


        [Theory]
        [InlineData("xxxx", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "hex", "hex", typeof(NotSupportedAlgorithmException))]
        [InlineData("", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "hex", "hex", typeof(NotSupportedAlgorithmException))]
        [Description("Test Decrypt API not supported algorithm response")]
        public void TestDecryptionNotSupportedAlgorithmResponse(string _algorithm, string _cipherData, string _key, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            SymmetricDecryptionRequest req = new()
            {
                Algorithm = _algorithm,
                CipherData = _cipherData,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<SymmetricDecryptionResponse> res = controller.Decrypt(req);
            Assert.IsType<BadRequestObjectResult>(res.Result);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);

        }

        [Fact]
        public void TestSupportedAlgorithmsResponseIsValidHTTP()
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            ActionResult<HashSet<string>> res = controller.GetSupportedAlgorithms();
            Assert.IsType<OkObjectResult>(res.Result);
        }

        [Fact]
        public void TestSupportedAlgorithmsValues()
        {
            HashSet<string> expectedResult = new()
        {
            {"aes_cbc_pkcs7"},{"aes_ecb_pkcs7"}
        };
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
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
        public void InvalidGetAlgorithmTests()
        {

            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            ActionResult<HashSet<string>> res = controller.GetSupportedAlgorithms();
            Assert.IsType<BadRequestObjectResult>(res.Result);

        }
    }
}
