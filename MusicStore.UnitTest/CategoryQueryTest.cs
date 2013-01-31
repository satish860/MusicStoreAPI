using FluentAssertions;
using MusicStore.Api;
using MusicStore.Api.Models;
using MusicStore.Api.Query;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.SelfHost;

namespace MusicStore.UnitTest
{
    [TestFixture]
    public class CategoryQueryTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            new MusicStoreBootStrap(new HttpSelfHostConfiguration("http://localhost:8008"))
              .ConfigureDatabaseForTest()
              .SeedDatabase();
        }

        [Test]
        public void Should_be_able_to_Get_the_CategoryViewModel()
        {
            IQueryFor<NullArgument, IEnumerable<CategoryCountViewModel>> categoryCountViewModel = new CategoryAndCountQuery();
            IEnumerable<CategoryCountViewModel> CategoryCount = categoryCountViewModel.Execute(NullArgument.BuildDefault());
            CategoryCount.Count().Should().Be(8);
        }

        [Test]
        public void Should_be_able_to_get_the_Countof_the_Category()
        {
            IQueryFor<NullArgument, IEnumerable<CategoryCountViewModel>> categoryCountViewModel = new CategoryAndCountQuery();
            IEnumerable<CategoryCountViewModel> CategoryCount = categoryCountViewModel.Execute(NullArgument.BuildDefault());
            CategoryCount.First().Count.Should().Be(4);
            CategoryCount.First().CategoryName.Should().Be("Mobile");
        }
    }
}