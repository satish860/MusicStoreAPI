using MusicStore.Api;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Net;

namespace MusicStore.UnitTest
{
    [TestFixture]
    public class CartControllerTest
    {
        [Test]
        public void Should_be_Send_the_Command_to_the_Command_bus()
        {
            dynamic Controller = ConfigureController(TestInstance.CartController);
            CreateCartCommand command = new CreateCartCommand { Id = 1 };
            Controller.Post(command);
            TestInstance.InMemoryCommandBus.Command.Should().Be(command);
        }

        [Test]
        public void Should_be_able_to_Send_back_Status_Code_As_201()
        {
            dynamic Controller = ConfigureController(TestInstance.CartController);
            CreateCartCommand command = new CreateCartCommand { Id = 1 };
            HttpResponseMessage message = Controller.Post(command);
            message.StatusCode.Should().Be(HttpStatusCode.Created);
            message.Headers.Location.ToString().Should().Be("http://localhost/api/Cart/1");
        }

        public ApiController ConfigureController(ApiController controller)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/products");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "products" } });
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            return controller;
        }
    }
}
