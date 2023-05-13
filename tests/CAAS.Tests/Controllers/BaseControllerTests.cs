using CAAS.Models.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.ComponentModel;

namespace CAAS.Tests.Controllers
{
    public class BaseControllerTests
    {
        [Fact]
        [Description("Test Health Check API returns valid http response")]
        public void TestHealthCheckResponseIsValidHTTP()
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.BaseController>>();
            CAAS.Controllers.BaseController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            ActionResult<HealthCheckResponse> res = controller.GetHealth();
            Assert.IsType<OkObjectResult>(res.Result);
        }

        [Fact]
        [Description("Test Health Check API returns a valid healthCheck Response value")]
        public void TestHealthCheckResponseValue()
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.BaseController>>();
            CAAS.Controllers.BaseController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            ActionResult<HealthCheckResponse> res = controller.GetHealth();
            Assert.IsType<OkObjectResult>(res.Result);
            HealthCheckResponse? responseObject = (res.Result as ObjectResult).Value as HealthCheckResponse;
            Assert.Equal("Iam Healthy", responseObject.Status);

        }

        [Fact]
        [Description("Test Health Check API returns expected status")]
        public void TestHealthCheckStatusField()
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.BaseController>>();
            CAAS.Controllers.BaseController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            ActionResult<HealthCheckResponse> res = controller.GetHealth();
            Assert.IsType<OkObjectResult>(res.Result);
            HealthCheckResponse? responseObject = (res.Result as ObjectResult).Value as HealthCheckResponse;
            Assert.Equal("Iam Healthy", responseObject.Status);

        }

        [Fact]
        [Description("Test Health Check API returns expected processingTime")]
        public void TestHealthCheckProcessingTimeField()
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.BaseController>>();
            CAAS.Controllers.BaseController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            ActionResult<HealthCheckResponse> res = controller.GetHealth();
            Assert.IsType<OkObjectResult>(res.Result);
            HealthCheckResponse? responseObject = (res.Result as ObjectResult).Value as HealthCheckResponse;
            Assert.True(responseObject.ProcessingTimeInMs >= 0);

        }

        [Fact]
        [Description("Test Healthcheck BadRequest, when no httpContext provided")]
        public void TestInvalidHealthCheckBadRequestResponse()
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.BaseController>>();
            CAAS.Controllers.BaseController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            ActionResult<HealthCheckResponse> res = controller.GetHealth();
            Assert.IsType<BadRequestObjectResult>(res.Result);
        }

        [Fact]
        [Description("Test DataFormats API returns valid http response")]
        public void TestDataFormatsResonseIsValidHTTP()
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.BaseController>>();
            CAAS.Controllers.BaseController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            ActionResult<HashSet<string>> res = controller.GetDataFormats();
            Assert.IsType<OkObjectResult>(res.Result);

        }

        [Fact]
        [Description("Test DataFormats API Response Object")]
        public void TestDataFormatsResponseValues()
        {
            HashSet<string> expectedResult = new()
        {
            {"hex" },{"base64" },{"ascii" },{"utf8"}
        };
            var logger = Mock.Of<ILogger<CAAS.Controllers.BaseController>>();
            CAAS.Controllers.BaseController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            ActionResult<HashSet<string>> res = controller.GetDataFormats();
            Assert.IsType<OkObjectResult>(res.Result);
            HashSet<string>? responseObject = (res.Result as ObjectResult).Value as HashSet<string>;
            Assert.Equal(responseObject, expectedResult);
        }

        [Fact]
        [Description("Test DataFormats BadRequest, when no httpContext provided")]
        public void TestInvalidDataFormatBadRequestResponse()
        {
            var logger = Mock.Of<ILogger<CAAS.Controllers.BaseController>>();
            CAAS.Controllers.BaseController controller = new(logger)
            {
                ControllerContext = new ControllerContext()
            };
            ActionResult<HashSet<string>> res = controller.GetDataFormats();
            Assert.IsType<BadRequestObjectResult>(res.Result);

        }
    }
}
