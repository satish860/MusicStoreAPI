using FluentAssertions;
using MusicStore.Api;
using MusicStore.Api.CommandHandlers;
using MusicStore.Api.Controllers;
using MusicStore.Api.Models;
using NUnit.Framework;
using Simple.Data;
using System;
using System.Web.Http.SelfHost;

namespace MusicStore.UnitTest
{
    [TestFixture]
    public class AddProductCommandUnitTest
    {
        [Test]
        public void Should_be_Able_to_Add_the_Product()
        {
            var baseAddress = new Uri("http://localhost:8082/");
            var httpSelfhostConfiguration = new HttpSelfHostConfiguration(baseAddress);
            new MusicStoreBootStrap(httpSelfhostConfiguration).ConfigureDatabaseForTest();
            AddProductCommand command = new AddProductCommand { Name = "Reebok Shoe", Categories = "Shoes", Id = 1 };
            ICommandHandler<AddProductCommand> productCommand = new AddProductCommandHandler();
            productCommand.Execute(command);
            ProductViewModel ViewModel = Database.Open().Products.FindById(1);
            ViewModel.Name.Should().Be(command.Name);
        }
    }
}