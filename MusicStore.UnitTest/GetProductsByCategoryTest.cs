using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using MusicStore.Api.Query;
using MusicStore.Api.Models;
using MusicStore.Api;
using System.Web.Http.SelfHost;

namespace MusicStore.UnitTest
{
    [TestFixture]
    public class GetProductsByCategoryTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            new MusicStoreBootStrap(new HttpSelfHostConfiguration("http://localhost:8008"))
              .ConfigureDatabaseForTest()
              .SeedDatabase();
        }
        [Test]
        public void Should_be_able_to_Get_the_products_By_Category()
        {
            IQueryFor<CategoryName, ListOfProducts> getallproductQuery = TestInstance.ProductsByCategoryQuery;
            ListOfProducts products=getallproductQuery.Execute(new CategoryName{Name="Mobile"});
            products.Products.Select(p => p.Id).Should().BeEquivalentTo(Enumerable.Range(1, 4));
        }

        [Test]
        public void Should_return_the_Count_of_the_Product()
        {
            IQueryFor<CategoryName, ListOfProducts> getallproductQuery = TestInstance.ProductsByCategoryQuery;
            ListOfProducts products = getallproductQuery.Execute(new CategoryName { Name = "Mobile" });
            products.PageCount.Should().Be(1);
        }

        [Test]
        public void Should_return_the_Zero_Result_When_there_is_no_Item_Found()
        {
            IQueryFor<CategoryName, ListOfProducts> getallproductQuery = TestInstance.ProductsByCategoryQuery;
            ListOfProducts products = getallproductQuery.Execute(new CategoryName { Name = "Mobile1" });
            products.PageCount.Should().Be(0);
        }
    }
}
