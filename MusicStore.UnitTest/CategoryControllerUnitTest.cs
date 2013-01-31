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
using System.Web.Http.SelfHost;
using MusicStore.Api.Models;

namespace MusicStore.UnitTest
{
    [TestFixture]
    public class CategoryControllerUnitTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            new MusicStoreBootStrap(new HttpSelfHostConfiguration("http://localhost:8008"))
              .ConfigureDatabaseForTest()
              .SeedDatabase();
        }

        [Test]
        public void Should_be_able_to_Get_the_Url_for_Products_By_Category()
        {
            dynamic Controller = ConfigureController(TestInstance.CategoryController);
            IEnumerable<CategoryCountViewModel> categoryCountModel=Controller.Get();
            categoryCountModel.First().ProductUrl.Should().Be("http://localhost/api/Products/Mobile");
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
