using MusicStore.Api.Models;
using MusicStore.Api.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Api;
using FluentAssertions;
using NUnit.Framework;
using System.Web.Http.SelfHost;

namespace MusicStore.UnitTest
{
    [TestFixture]
    public class ProductQueryTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            new MusicStoreBootStrap(new HttpSelfHostConfiguration("http://localhost:8008"))
              .ConfigureDatabaseForTest()
              .SeedDatabase();
        }

        [Test]
        public void Should_be_able_to_Query_for_the_First_product_without_send_Page_Number()
        {
            IQueryFor<PagedResult, ListOfProducts> Query = TestInstance.ProductQuery;
            ListOfProducts ProductList = Query.Execute(PagedResult.ForPage(1));
            ProductList.Products.Select(p => p.Id).Should().ContainInOrder(Enumerable.Range(1, 10));
        }

        [Test]
        public void Should_be_able_to_Query_for_Page_numbers_with_it()
        {
            IQueryFor<PagedResult, ListOfProducts> Query = TestInstance.ProductQuery;
            ListOfProducts ProductList = Query.Execute(PagedResult.ForPage(2));
            ProductList.Products.Select(p => p.Id).Should().ContainInOrder(Enumerable.Range(11, 8));
        }

        [Test]
        public void Should_be_able_to_Get_the_Total_Count_of_Products_only_for_the_first_time()
        {
            IQueryFor<PagedResult, ListOfProducts> Query = TestInstance.ProductQuery;
            ListOfProducts ProductList = Query.Execute(PagedResult.ForPage(1));
            ProductList.PageCount.Should().Be(2);
        }

        [Test]
        public void Should_not_Return_total_Count_if_the_Page_number_is_Not_1()
        {
            IQueryFor<PagedResult, ListOfProducts> Query = TestInstance.ProductQuery;
            ListOfProducts ProductList = Query.Execute(PagedResult.ForPage(2));
            ProductList.PageCount.Should().Be(2);
        }
    }
}
