using MusicStore.Api.Models;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.Api.CommandHandlers
{
    public class AddProductToCartCommandHandler : ICommandHandler<AddProductToCartCommand>
    {
        public void Execute(AddProductToCartCommand Command)
        {
            var Db = Database.Open();
            Cart cart = Db.Cart.FindById(Command.CartId);
            IEnumerable<CartItem> items = Db.CartItem.FindAllByCartId(Command.CartId);
            cart.CartItems = items==null?new List<CartItem>():items.ToList();
            Product product = Db.Products.FindById(Command.ProductId);
            cart.AddItemToCart(product, Command.Quantity);
            foreach (var item in cart.CartItems)
            {
                if (item.Isnew)
                {
                    item.Isnew = false;
                    Db.CartItem.Insert(item);
                }
                else
                {
                    Db.CartItem.UpdateByCartId(item);
                }
            }
            Db.Cart.UpdateById(new Cart { Id = cart.Id, Price = cart.Price });
        }
    }
}