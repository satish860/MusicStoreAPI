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

            it["Should be possible to add a product to the cart"] = () =>
            {
                var client = HttpClientFactory.CreateClient();
                CreateCartCommand Createcommand = new CreateCartCommand();
                Createcommand.Id = 1;
                client.PostAsJsonAsync("api/Cart", Createcommand);
                var command = new { cartId = 1, productId = 1, quantity = 1 };
                var Response = client.PutAsJsonAsync("api/Cart", command).Result;
                Response.StatusCode.should_be(HttpStatusCode.OK);
            };

            it["Should be possible to increase the quantity of the product in the cart"] = () =>
            {
                var client = HttpClientFactory.CreateClient();
                CreateCartCommand Createcommand = new CreateCartCommand();
                Createcommand.Id = 1;
                client.PostAsJsonAsync("api/Cart", Createcommand);
                AddProductToCartCommand command = new AddProductToCartCommand { CartId = 1, ProductId = 1, Quantity = 2 };
                var Response = client.PutAsJsonAsync("api/Cart", command).Result;
                Response.StatusCode.should_be(HttpStatusCode.OK);
            };

            xit["Should be able to delete the Product from the Cart"] = () =>
            {
                var client = HttpClientFactory.CreateClient();
                CreateCartCommand Createcommand = new CreateCartCommand();
                Createcommand.Id = 1;
                client.PostAsJsonAsync("api/Cart", Createcommand);
                AddProductToCartCommand command = new AddProductToCartCommand { CartId = 1, ProductId = 1, Quantity = 2 };
                client.PutAsJsonAsync("api/Cart", command);
                var DeleteCommand = new { cartId = 1, productId = 1, quantity = 0 };
                var Response = client.PutAsJsonAsync("api/Cart", DeleteCommand).Result;
                Response.StatusCode.should_be(HttpStatusCode.NoContent);
            };
        }
    }
}