using CAAS.Exceptions;
using CAAS.Models.Rng;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CAAS.Tests.Controllers
{
    public class RngControllerTests
    {
        [Theory]
        [InlineData("csprng", 1, "hex")]
        [InlineData("csprng", 0, "hex")]
        [InlineData("csprng", 16, "hex")]
        [InlineData("csprng", 20, "hex")]
        public void GenerateTests(string _algorithm, int _size, string _outputDataFormat)
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
            Assert.NotNull(res);
            Assert.NotNull(res.Result as OkObjectResult);
            RngResponse? responseObject = (res.Result as ObjectResult).Value as RngResponse;
            Assert.NotNull(responseObject);
            Assert.True(responseObject.Rng.Length/2 == _size);
            Assert.True(responseObject.ProcessingTimeInMs >= 0);

        }
        [Theory]
        [InlineData("csprng", 1, "xxx", typeof(NotSupportedDataFormatException))]
        [InlineData("xxx", 1, "hex", typeof(NotSupportedAlgorithmException))]
        [InlineData("csprng", -9, "hex", typeof(OverflowException))]
        public void InvalidGenerateTests(string _algorithm, int _size, string _outputDataFormat, Type _exceptionType)
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
            {"csprng" }
        };
            var logger = Mock.Of<ILogger<CAAS.Controllers.RngController>>();
            CAAS.Controllers.RngController controller = new(logger)
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

            var logger = Mock.Of<ILogger<CAAS.Controllers.RngController>>();
            CAAS.Controllers.RngController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            ActionResult<HashSet<string>> res = controller.GetSupportedAlgorithms();
            Assert.NotNull(res);
            Assert.NotNull(res.Result as BadRequestObjectResult);

        }

    }
}
