using MusicStore.Api.Models;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.Api.CommandHandlers
{
    public class RemoveItemFromCartCommandHandler : ICommandHandler<DeleteItemFromCartCommand>
    {
        public void Execute(DeleteItemFromCartCommand Command)
        {
            var Db = Database.Open();
            Cart cart = Db.Cart.FindById(Command.CartId);
            IEnumerable<CartItem> items = Db.CartItem.FindAllByCartId(Command.CartId);
            cart.CartItems = items == null ? new List<CartItem>() : items.ToList();
            cart.RemoveItem(Command.ProductId);
            foreach (var item in cart.CartItems)
            {
                if (item.IsDelete)
                    Db.CartItem.DeleteAll(Db.CartItem.CartId == item.CartId && Db.CartItem.ProductId == item.ProductId);
            }
            Db.Cart.Update(cart);
        }
    }
}