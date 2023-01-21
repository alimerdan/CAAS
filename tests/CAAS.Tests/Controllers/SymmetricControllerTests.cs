using CAAS.Exceptions;
using CAAS.Models.Symmetric.Decryption;
using CAAS.Models.Symmetric.Encryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CAAS.Tests.Controllers
{
    public class SymmetricControllerTests
    {
        [Theory]
        [InlineData("aes_cbc_pkcs7", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", "C656C652E6656125139C219FD9F6EABB")]
        [InlineData("aes_ecb_pkcs7", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", "C656C652E6656125139C219FD9F6EABB")]
        public void EncryptTests(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, string _expectedResult)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            EncryptionRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<EncryptionResponse> res = controller.Encrypt(req);
            Assert.NotNull(res);
            Assert.NotNull(res.Result as OkObjectResult);
            EncryptionResponse? responseObject = (res.Result as ObjectResult).Value as EncryptionResponse;
            Assert.NotNull(responseObject);
            Assert.Equal(_expectedResult, responseObject.CipherData);
            Assert.True(responseObject.ProcessingTimeInMs >= 0);

        }

        [Theory]
        [InlineData("aes_cbc_pkcs7", "0011223344556677", "0011223344556677", "hex", "hex", typeof(Exception))]
        [InlineData("aes_ecb_pkcs7", "0011223344556677", "0011223344556677", "hex", "hex", typeof(Exception))]
        [InlineData("aes_ecb_pkcs7", "0011223344556677", "00112233445566770011223344556677", "xxxx", "hex", typeof(NotSupportedDataFormatException))]
        [InlineData("aes_ecb_pkcs7", "0011223344556677", "00112233445566770011223344556677", "hex", "xxxx", typeof(NotSupportedDataFormatException))]
        [InlineData("xxxx", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", typeof(NotSupportedAlgorithmException))]
        public void InvalidEncryptionTests(string _algorithm, string _data, string _key, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            EncryptionRequest req = new()
            {
                Algorithm = _algorithm,
                Data = _data,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<EncryptionResponse> res = controller.Encrypt(req);
            Assert.NotNull(res);
            Assert.NotNull(res.Result as BadRequestObjectResult);
            ErrorResponse? responseObject = (res.Result as ObjectResult).Value as ErrorResponse;
            Assert.NotNull(responseObject);
            Assert.Equal(_exceptionType.FullName, responseObject.ExceptionType);

        }

        [Theory]
        [InlineData("aes_cbc_pkcs7", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "hex", "hex", "0011223344556677")]
        [InlineData("aes_ecb_pkcs7", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "hex", "hex", "0011223344556677")]
        public void DecryptionTests(string _algorithm, string _cipherData, string _key, string _inputDataFormat, string _outputDataFormat, string _expectedResult)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            DecryptionRequest req = new()
            {
                Algorithm = _algorithm,
                CipherData = _cipherData,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<DecryptionResponse> res = controller.Decrypt(req);
            Assert.NotNull(res);
            Assert.NotNull(res.Result as OkObjectResult);
            DecryptionResponse? responseObject = (res.Result as ObjectResult).Value as DecryptionResponse;
            Assert.NotNull(responseObject);
            Assert.Equal(_expectedResult, responseObject.Data);
            Assert.True(responseObject.ProcessingTimeInMs >= 0);

        }

        [Theory]
        [InlineData("aes_cbc_pkcs7", "C656C652E6656125139C219FD9F6EABB", "0011223344556677", "hex", "hex", typeof(Exception))]
        [InlineData("aes_ecb_pkcs7", "C656C652E6656125139C219FD9F6EABB", "0011223344556677", "hex", "hex", typeof(Exception))]
        [InlineData("aes_cbc_pkcs7", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "hex", "xxxx", typeof(NotSupportedDataFormatException))]
        [InlineData("aes_cbc_pkcs7", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "xxxx", "hex", typeof(NotSupportedDataFormatException))]
        [InlineData("xxxx", "C656C652E6656125139C219FD9F6EABB", "00112233445566770011223344556677", "hex", "hex", typeof(NotSupportedAlgorithmException))]
        public void InvalidDecryptionTests(string _algorithm, string _cipherData, string _key, string _inputDataFormat, string _outputDataFormat, Type _exceptionType)
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            DecryptionRequest req = new()
            {
                Algorithm = _algorithm,
                CipherData = _cipherData,
                Key = _key,
                InputDataFormat = _inputDataFormat,
                OutputDataFormat = _outputDataFormat

            };
            ActionResult<DecryptionResponse> res = controller.Decrypt(req);
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
            {"aes_cbc_pkcs7"},{"aes_ecb_pkcs7"}
        };
            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
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

            var logger = Mock.Of<ILogger<CAAS.Controllers.SymmetricController>>();
            CAAS.Controllers.SymmetricController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            ActionResult<HashSet<string>> res = controller.GetSupportedAlgorithms();
            Assert.NotNull(res);
            Assert.NotNull(res.Result as BadRequestObjectResult);

        }
    }
}
