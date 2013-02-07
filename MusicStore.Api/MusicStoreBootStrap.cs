using MusicStore.Api.Command;
using MusicStore.Api.CommandHandlers;
using MusicStore.Api.Models;
using MusicStore.Api.Query;
using Newtonsoft.Json.Serialization;
using Simple.Data;
using StructureMap;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Simple.Data.SqlServer;
using System.Data.SqlClient;

namespace MusicStore.Api
{
    public class MusicStoreBootStrap
    {
        private readonly HttpConfiguration httpConfiguration;
        private IContainer container;

        public MusicStoreBootStrap(HttpConfiguration configuration)
        {
            this.httpConfiguration = configuration;
        }

        public object IQueryFor { get; set; }

        public MusicStoreBootStrap ConfigureContainer()
        {
            container = new Container();
            httpConfiguration.Services.Replace(typeof(IHttpControllerActivator), new HttpActivator(container));
            container.Configure(configure =>
            {
                configure.For<ICommandBus>().Use<CommandBus>();
                configure.Scan(Scanner =>
                {
                    Scanner.AssemblyContainingType<AddProductCommandHandler>();
                    Scanner.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
                    Scanner.ConnectImplementationsToTypesClosing(typeof(IQueryFor<,>));
                });
            });
            return this;
        }


        public MusicStoreBootStrap ConfigureDatabaseForTest()
        {
            var adapter = new InMemoryAdapter();
            adapter.SetKeyColumn("AdminUser", "UserName");
            adapter.SetKeyColumn("Products", "Id");
            adapter.SetKeyColumn("Cart", "Id");
            adapter.SetKeyColumn("CartItem", "CartId");
            Database.UseMockAdapter(adapter);
            return this;
        }

        public MusicStoreBootStrap ConfigureRoute()
        {
            // List of delegating handlers.
            DelegatingHandler[] handlers = new DelegatingHandler[] { new AdminAuthorizationMessageHandler(new AdminUserQuery()) };

            

            // Create a message handler chain with an end-point.
            var routeHandlers = HttpClientFactory.CreatePipeline(
                new HttpControllerDispatcher(httpConfiguration), handlers);
            httpConfiguration.Routes.MapHttpRoute(
                name: "AdminRoutes",
                routeTemplate: "api/AdminUser/{user}",
                defaults: new { controller = "AdminUser", user = RouteParameter.Optional }
                , constraints: null
                , handler: routeHandlers);

            httpConfiguration.Routes.MapHttpRoute(
              name: "Inventory",
              routeTemplate: "api/Inventory/{id}",
              defaults: new { controller = "Inventory", id = RouteParameter.Optional }
              , constraints: null
              , handler: routeHandlers);

            httpConfiguration.Routes.MapHttpRoute(
             name: "ActionApi",
             routeTemplate: "api/{controller}/{action}/{id}",
             defaults: new { controller = "Product", id = RouteParameter.Optional });

            httpConfiguration.Routes.MapHttpRoute(
             name: "DefaultApi",
             routeTemplate: "api/{controller}/{id}",
             defaults: new { controller = "Product", id = RouteParameter.Optional });
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            
            return this;
        }

        public MusicStoreBootStrap SeedDatabase()
        {

            var database = Database.Open();
            database.AdminUser.DeleteAll();
            database.Products.DeleteAll();
            AdminModel adminUser = new AdminModel();
            adminUser.UserName = "Admin";
            adminUser.Password = "Admin";
            database.AdminUser.Insert(adminUser);
            database.Products.Insert(new Product { Name = "Samsung s2", Id = 1, Categories = "Mobile", Price = 20000 });
            database.Products.Insert(new Product { Name = "HTC sensation", Id = 2, Categories = "Mobile", Price = 30000 });
            database.Products.Insert(new Product { Name = "Nokia lumia", Id = 3, Categories = "Mobile", Price = 10000 });
            database.Products.Insert(new Product { Name = "sony experia", Id = 4, Categories = "Mobile", Price = 1000 });
            database.Products.Insert(new Product { Name = "samsung series 9", Id = 5, Categories = "Laptop", Price = 30000.00 });
            database.Products.Insert(new Product { Name = "Dell xps", Id = 6, Categories = "Laptop", Price = 40000.00 });
            database.Products.Insert(new Product { Name = "HP envy", Id = 7, Categories = "Laptop", Price = 50000.00 });
            database.Products.Insert(new Product { Name = "Sony vaio", Id = 8, Categories = "Laptop", Price = 60000.00 });
            database.Products.Insert(new Product { Name = "Samsung Camera", Id = 9, Categories = "Camera", Price = 10000.00 });
            database.Products.Insert(new Product { Name = "Sony", Id = 10, Categories = "Camera", Price = 11000.00 });
            database.Products.Insert(new Product { Name = "Legend of Mehula", Id = 11, Categories = "Books", Price = 100.00 });
            database.Products.Insert(new Product { Name = "2 States", Id = 12, Categories = "Books", Price = 100.00 });
            database.Products.Insert(new Product { Name = "Router", Id = 13, Categories = "Home Accessories", Price = 1000.00 });
            database.Products.Insert(new Product { Name = "Webcam", Id = 14, Categories = "Home Accessories", Price = 1000.00 });
            database.Products.Insert(new Product { Name = "PenDrive", Id = 15, Categories = "Electronics", Price = 1000.00 });
            database.Products.Insert(new Product { Name = "Electric Stove", Id = 16, Categories = "Kitchen", Price = 450.00 });
            database.Products.Insert(new Product { Name = "Sony", Id = 17, Categories = "TV", Price = 25000.00 });
            database.Products.Insert(new Product { Name = "LG", Id = 18, Categories = "TV", Price = 25000.00 });
            return this;
        }
    }
}