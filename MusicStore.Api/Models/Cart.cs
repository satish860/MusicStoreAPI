using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.Api.Models
{
    public class Cart
    {

        public int Id { get; set; }

        public double Price { get; set; }

        public IList<CartItem> CartItems { get; set; }

        public void AddItemToCart(Product item, int Quantity)
        {
            CartItem cartItem = CartItems.FirstOrDefault(p => p.ProductId == item.Id);
            if (cartItem == null)
            {
                cartItem = new CartItem { Price = (Quantity * item.Price), ProductId = item.Id, ProductName = item.Name, Quantity = Quantity, CartId = Id, Isnew = true };
                CartItems.Add(cartItem);
            }
            else
                cartItem.Price = Quantity * item.Price;
            Price = CartItems.Select(p => p.Price).Sum();
        }

        public void RemoveItem(int productId)
        {
            CartItem Item=CartItems.First(p => p.ProductId == productId);
            if (Item != null)
                Item.IsDelete = true;
            Price = CartItems.Where(p=>!p.IsDelete).Select(p => p.Price).Sum();
        }
    }
}