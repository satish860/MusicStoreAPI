using MusicStore.Api.Controllers;
using Simple.Data;

namespace MusicStore.Api.CommandHandlers
{
    public class AddProductCommandHandler : ICommandHandler<AddProductCommand>
    {
        public void Execute(AddProductCommand Command)
        {
            var db = Database.Open();
            db.Products.Insert(Id: Command.Id, Name: Command.Name, Categories: Command.Categories);
        }
    }
}