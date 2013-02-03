using MusicStore.Api.CommandHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Api;
using Simple.Data;
using FluentAssertions;
using NUnit.Framework;
using System.Web.Http.SelfHost;
using MusicStore.Api.Models;

namespace MusicStore.UnitTest
{
    [TestFixture]
    public class DeleteCommandHandlerUnitTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            new MusicStoreBootStrap(new HttpSelfHostConfiguration("http://localhost:8008"))
              .ConfigureDatabaseForTest()
              .SeedDatabase();
        }

        [SetUp]
        public void testSetUp()
        {
            var Db = Database.Open();
            Db.Cart.Insert(new Cart { Id = 1 });
        }

        [TearDown]
        public void TearDown()
        {
            var Db = Database.Open();
            Db.CartItem.DeleteByCartId(1);
            Db.Cart.DeleteById(1);
        }


        [Test]
        public void Should_be_able_to_Delete_the_Cart_Item_from_the_Cart()
        {
            ICommandHandler<AddProductToCartCommand> command = TestInstance.AddProductToCommandHandler;
            command.Execute(new AddProductToCartCommand { CartId = 1, ProductId = 1, Quantity = 1 });
            ICommandHandler<DeleteItemFromCartCommand> CommandHandler = TestInstance.DeleteCommandHandler;
            CommandHandler.Execute(new DeleteItemFromCartCommand { CartId = 1, ProductId = 1 });
            var Db = Database.Open();
            IEnumerable<CartItem> items = Db.CartItem.FindAllByCartId(1);
            Cart cart=Db.Cart.FindById(1);
            cart.Price.Should().Be(0);
            items.Count().Should().Be(0);
        }
    }
}
