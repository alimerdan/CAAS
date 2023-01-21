using CAAS.Models.Symmetric.Encryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAAS.Tests.Controllers
{
    public class SymmetricControllerTests
    {
        [Theory]
        [InlineData("aes_cbc", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", "C656C652E6656125139C219FD9F6EABB")]
        [InlineData("aes_ecb", "0011223344556677", "00112233445566770011223344556677", "hex", "hex", "C656C652E6656125139C219FD9F6EABB")]
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
    }
}
