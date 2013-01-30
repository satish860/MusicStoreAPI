using NSpec;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MusicStore.AcceptanceTest
{
    public class AdminCanAddProductToInventorySpec : nspec
    {
        public void only_Admin_Can_Add_the_product()
        {
            it["Should Return 401 if he is not an admin"] = () =>
            {
                var client = HttpClientFactory.CreateClient();
                var product = new { id = 1, name = "Reebok Shoes", category = "Shoes" };
                var Response = client.PostAsJsonAsync("/api/Inventory", product).Result;
                Response.StatusCode.should_be(HttpStatusCode.Unauthorized);
            };

            it["Should return 201 with the Product URL as the Response"] = () =>
            {
                var client = HttpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", "QWRtaW46QWRtaW4=");
                var product = new { id = 1, name = "Reebok Shoes", category = "Shoes" };
                var Response = client.PostAsJsonAsync("/api/Inventory", product).Result;
                Response.StatusCode.should_be(HttpStatusCode.Created);
                Response.Headers.Location.ToString().should_be("http://localhost:8082/api/Product/1");
            };
            it["Should be possible to add Multiple Categories with comma seperated string"] = () =>
            {
                var client = HttpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", "QWRtaW46QWRtaW4=");
                var product = new { id = 2, name = "", category = "Shoes" };
                var Response = client.PostAsJsonAsync("/api/Inventory", product).Result;
                Response.StatusCode.should_be(HttpStatusCode.Created);
                Response.Headers.Location.ToString().should_be("http://localhost:8082/api/Product/2");
            };

            it["Should be possible to add the Images to the Particular product with the ID"] = () =>
            {
            };

            xit["Should Return 400 with the validation error message in the content for the Product Validation Error"] = () =>
            {
            };
        }
    }
}