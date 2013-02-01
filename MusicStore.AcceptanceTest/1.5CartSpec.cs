using MusicStore.Api;
using NSpec;
using System.Net;
using System.Net.Http;

namespace MusicStore.AcceptanceTest
{
    public class CartSpec : nspec
    {
        public void Given_the_Application_is_Intialized()
        {
            it["Should create a cart and the intialize the Id for the same"] = () =>
            {
                var client = HttpClientFactory.CreateClient();
                CreateCartCommand command = new CreateCartCommand();
                command.Id = 1;
                var Response = client.PostAsJsonAsync("api/Cart", command).Result;
                Response.StatusCode.should_be(HttpStatusCode.Created);
                Response.Headers.Location.ToString().should_be("http://localhost:8082/api/Cart/1");
            };
        }
    }
}