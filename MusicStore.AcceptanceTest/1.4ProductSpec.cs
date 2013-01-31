using FluentAssertions;
using MusicStore.Api.Models;
using Newtonsoft.Json.Linq;
using NSpec;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace MusicStore.AcceptanceTest
{
    public class ProductsSpec : nspec
    {
        public void Given_the_List_of_Products_added_to_the_Database()
        {
            it["Should be able to get upto first 10 products without sending page number"] = () =>
            {
                var client = HttpClientFactory.CreateClient();
                HttpResponseMessage message = client.GetAsync("/api/products/GetAllProducts").Result;
                JObject items = JObject.Parse(message.Content.ReadAsStringAsync().Result);
                ListOfProducts productViewModels = items.ToObject<ListOfProducts>();
                productViewModels.Products.Select(p => p.Id).Should().ContainInOrder(Enumerable.Range(1, 10));
            };

            it["Should be able to get 10 products based on the page Number"] = () =>
            {
                var client = HttpClientFactory.CreateClient();
                HttpResponseMessage message = client.GetAsync("/api/products/GetAllProducts/2").Result;
                JObject items = JObject.Parse(message.Content.ReadAsStringAsync().Result);
                ListOfProducts productViewModels = items.ToObject<ListOfProducts>();
                productViewModels.Products.Select(p => p.Id).Should().ContainInOrder(Enumerable.Range(1, 10));
            };

            xit["Should be able to get the details about the product by selecting the project"] = () =>
            {
            };
        }
    }
}