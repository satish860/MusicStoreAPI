using MusicStore.Api;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace MusicStore.AcceptanceTest
{
    public static class HttpClientFactory
    {
        public static HttpClient CreateClient()
        {
            var baseAddress = new Uri("http://localhost:8082/");
            var httpSelfhostConfiguration = new HttpSelfHostConfiguration(baseAddress);
            httpSelfhostConfiguration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            HttpSelfHostServer server = new HttpSelfHostServer(httpSelfhostConfiguration);
            new MusicStoreBootStrap(httpSelfhostConfiguration)
                .ConfigureRoute()
                .ConfigureContainer()
                .ConfigureDatabaseForTest()
                .SeedDatabase();
            HttpClient client = new HttpClient(server);
            client.BaseAddress = baseAddress;
            return client;
        }
    }
}