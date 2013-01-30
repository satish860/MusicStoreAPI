using FluentAssertions;
using MusicStore.Api;
using Newtonsoft.Json.Linq;
using NSpec;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http.SelfHost;
using System.Linq;

namespace MusicStore.AcceptanceTest
{
    public class CategorySpec : nspec
    {
        public void Given_all_the_Products_With_Category()
        {
            JArray ActualResult = null;
            IEnumerable<CategoryCountViewModel> ExpectedResult = null;
            IEnumerable<CategoryCountViewModel> ActualModel = null;

            beforeAll = () =>
            {
                var client = HttpClientFactory.CreateClient();
                HttpSelfHostConfiguration configuration = new HttpSelfHostConfiguration("http://locahost:8080");

                var response = client.GetAsync("/api/category").Result;
                ExpectedResult = new List<CategoryCountViewModel> {
                    new CategoryCountViewModel{CategoryName="Mobile",Count=4},
                    new CategoryCountViewModel{CategoryName="Laptop",Count=4},
                      new CategoryCountViewModel{CategoryName="Camera",Count=2},
                      new CategoryCountViewModel{CategoryName="Home Accesories",Count=2},
                       new CategoryCountViewModel{CategoryName="Books",Count=2},
                           new CategoryCountViewModel{CategoryName="Electronics",Count=1},
                            new CategoryCountViewModel{CategoryName="Kitchen",Count=1},
                            new CategoryCountViewModel{CategoryName="TV",Count=2}
                };

                ActualResult = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                ActualModel = ActualResult.ToObject<IEnumerable<CategoryCountViewModel>>();

            };


            it["Should be possible to get all the category"] = () =>
            {
                ActualModel.Should().BeEquivalentTo(ExpectedResult);
            };

            it["Should be possible to get all the Count of the Category"] = () =>
            {
                ActualModel.First().Count.should_be(4);
            };

            xit["Should be possible to get the url for Products By category"] = () =>
            {

            };
        }
    }
}