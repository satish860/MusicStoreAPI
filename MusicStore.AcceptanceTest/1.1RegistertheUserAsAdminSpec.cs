using NSpec;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MusicStore.AcceptanceTest
{
    public class RegistertheUserAsAdminSpec : nspec
    {
        private HttpClient client = null;

        public void Check_if_the_User_Already_Exist_With_user_name()
        {
            HttpResponseMessage response = null;
            beforeEach = () =>
            {
                client = HttpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", "QWRtaW46QWRtaW4=");
                var adminLogin = new { username = "admin", password = "password" };
                response = client.PostAsJsonAsync("api/AdminUser", adminLogin).Result;
            };
            it["Should Return 200  Bad request since the user is already availble"] = () =>
            {
                response = client.GetAsync("api/AdminUser/admin").Result;
                response.StatusCode.should_be(HttpStatusCode.OK);
            };

            it["Should Return 404 as the User is not Found"] = () =>
            {
                response = client.GetAsync("api/AdminUser/adminuser").Result;
                response.StatusCode.should_be(HttpStatusCode.NotFound);
            };
        }

        public void Only_Admin_Can_Add_the_Other_Admins()
        {
            it["Should return 401 if the request is unauthorized"] = () =>
            {
                client = HttpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = null;
                HttpResponseMessage response = client.GetAsync("api/AdminUser/admin").Result;
                response.StatusCode.should_be(HttpStatusCode.Unauthorized);
            };
        }

        public void When_We_Post_admin_user_name_And_password()
        {
            HttpResponseMessage response = null;
            beforeEach = () =>
            {
                client = HttpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", "QWRtaW46QWRtaW4=");
                var adminLogin = new { username = "satish", password = "password" };
                response = client.PostAsJsonAsync("api/AdminUser", adminLogin).Result;
            };
            it["Should Return 201 as Status code if the user is registerd"] = () =>
            {
                response.StatusCode.should_be(HttpStatusCode.Created);
            };

            it["Should Return 409 if the user already exist"] = () =>
            {
                var adminLogin = new { username = "satish", password = "password" };
                response = client.PostAsJsonAsync("api/AdminUser", adminLogin).Result;
                response.StatusCode.should_be(HttpStatusCode.Conflict);
            };
        }
    }
}