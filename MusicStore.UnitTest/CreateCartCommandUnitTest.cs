using MusicStore.Api;
using MusicStore.Api.CommandHandlers;
using MusicStore.Api.Models;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using System.Web.Http.SelfHost;

namespace MusicStore.UnitTest
{
    [TestFixture]
    public class CreateCartCommandUnitTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            new MusicStoreBootStrap(new HttpSelfHostConfiguration("http://localhost:8008"))
              .ConfigureDatabaseForTest()
              .SeedDatabase();
        }

        [Test]
        public void Should_be_able_to_Create_the_Command()
        {
            ICommandHandler<CreateCartCommand> CreateCartCommandHandler = TestInstance.CreateCartCommandHandler;
            CreateCartCommandHandler.Execute(new CreateCartCommand { Id = 1 });
            Cart cart=Database.Open().Cart.FindById(1);
            cart.Id.Should().Be(1);
        }
    }
}
