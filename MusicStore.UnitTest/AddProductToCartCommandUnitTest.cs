using MusicStore.Api;
using MusicStore.Api.CommandHandlers;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MusicStore.Api.Models;
using NUnit.Framework;
using System.Web.Http.SelfHost;

namespace MusicStore.UnitTest
{
    [TestFixture]
    public class AddProductToCartCommandUnitTest
    {
        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            new MusicStoreBootStrap(new HttpSelfHostConfiguration("http://localhost:8008"))
              .ConfigureDatabaseForTest()
              .SeedDatabase();
           
        }

        [SetUp]
        public void SetUp()
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
        public void Should_be_able_to_Add_The_Item_to_The_Cart()
        {
            ICommandHandler<AddProductToCartCommand> command = TestInstance.AddProductToCommandHandler;
            command.Execute(new AddProductToCartCommand { CartId = 1, ProductId = 1, Quantity = 1 });
            CartItem Item=Database.Open().CartItem.FindByCartId(1);
            Item.Price.Should().Be(20000);
            Item.ProductName.Should().Be("Samsung s2");
        }

        [Test]
        public void After_Adding_the_Item_to_the_Cart_We_Should_be_able_to_Calculate_price()
        {
            ICommandHandler<AddProductToCartCommand> command = TestInstance.AddProductToCommandHandler;
            command.Execute(new AddProductToCartCommand { CartId = 1, ProductId = 1, Quantity = 2 });
            CartItem Item = Database.Open().CartItem.FindByCartId(1);
            Item.Price.Should().Be(40000);
            Item.ProductName.Should().Be("Samsung s2");
        }

        [Test]
        public void After_adding_Two_products_the_Cart_Item_price_Should_be_Caluclated()
        {
            ICommandHandler<AddProductToCartCommand> command = TestInstance.AddProductToCommandHandler;
            command.Execute(new AddProductToCartCommand { CartId = 1, ProductId = 1, Quantity = 2 });
            command.Execute(new AddProductToCartCommand { CartId = 1, ProductId = 2, Quantity = 2 });
            Cart Item = Database.Open().Cart.FindById(1);
            Item.Price.Should().Be(100000);
        }

        [Test]
        public void If_the_Item_Is_already_availble_the_it_Should_Just_Change_the_Quantity()
        {
            ICommandHandler<AddProductToCartCommand> command = TestInstance.AddProductToCommandHandler;
            command.Execute(new AddProductToCartCommand { CartId = 1, ProductId = 1, Quantity = 1 });
            command.Execute(new AddProductToCartCommand { CartId = 1, ProductId = 1, Quantity = 3 });
            CartItem Item = Database.Open().CartItem.FindByCartId(1);
            Item.Price.Should().Be(60000);
            Item.ProductName.Should().Be("Samsung s2");

        }

    }
}
