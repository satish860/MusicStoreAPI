using FluentAssertions;
using MusicStore.Api;
using MusicStore.Api.Controllers;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

namespace MusicStore.UnitTest
{
    [TestFixture]
    public class InventoryControllerUnitTest
    {
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

        [Test]
        public void Should_be_able_Send_the_Command_to_the_Bus()
        {
            AddProductCommand command = new AddProductCommand { Name = "Reebok Shoe", Categories = "Shoes", Id = 1 };
            InMemoryCommandBus Bus = new InMemoryCommandBus();
            InventoryController controller = new InventoryController(Bus);
            ConfigureController(controller);
            HttpResponseMessage Message = controller.Post(command);
            Message.StatusCode.Should().Be(HttpStatusCode.Created);
            Message.Headers.Location.ToString().Should().Be("http://localhost/api/Product/1");
            Bus.Command.Should().Be(command);
        }
    }
}