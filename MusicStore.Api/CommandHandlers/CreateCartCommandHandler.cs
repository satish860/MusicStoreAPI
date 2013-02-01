using MusicStore.Api.Models;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.Api.CommandHandlers
{
    public class CreateCartCommandHandler:ICommandHandler<CreateCartCommand>
    {
        public void Execute(CreateCartCommand Command)
        {
            var Db = Database.Open();
            Db.Cart.Insert(new Cart { Id = Command.Id });
        }
    }
}