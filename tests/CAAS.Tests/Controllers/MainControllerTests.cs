using CAAS.Controllers;
using CAAS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CAAS.Tests.Controllers
{
    public class MainControllerTests
    {
        [Fact]
        public void HealthTests()
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.Controller>>();
            CAAS.Controllers.Controller controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            ActionResult<HealthCheckResponse> res = controller.GetHealth();
            Assert.NotNull(res);
            Assert.NotNull(res.Result as OkObjectResult);
            HealthCheckResponse? responseObject = (res.Result as ObjectResult).Value as HealthCheckResponse;
            Assert.NotNull(responseObject);
            Assert.Equal("Iam Healthy", responseObject.Status);
            Assert.True(responseObject.ProcessingTimeInMs >= 0);

        }
        [Fact]
        public void InvalidHealthTests()
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.Controller>>();
            CAAS.Controllers.Controller controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            ActionResult<HealthCheckResponse> res = controller.GetHealth();
            Assert.NotNull(res);
            Assert.NotNull(res.Result as BadRequestObjectResult);

        }
        [Fact]
        public void DataFormatsTests()
        {
            HashSet<string> expectedResult = new()
        {
            {"hex" },{"base64" },{"ascii" },{"utf8"}
        };
            var logger = Mock.Of<ILogger<CAAS.Controllers.Controller>>();
            CAAS.Controllers.Controller controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            ActionResult<HashSet<string>> res = controller.GetDataFormats();
            Assert.NotNull(res);
            Assert.NotNull(res.Result as OkObjectResult);
            HashSet<string>? responseObject = (res.Result as ObjectResult).Value as HashSet<string>;
            Assert.NotNull(responseObject);
            Assert.Equal(responseObject,expectedResult);

        }
        [Fact]
        public void InvalidDataFormatsTests()
        {

            var logger = Mock.Of<ILogger<CAAS.Controllers.Controller>>();
            CAAS.Controllers.Controller controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            ActionResult<HashSet<string>> res = controller.GetDataFormats();
            Assert.NotNull(res);
            Assert.NotNull(res.Result as BadRequestObjectResult);

        }
    }
}
