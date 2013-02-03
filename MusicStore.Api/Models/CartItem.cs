using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace MusicStore.Api
{
    public class CartItem
    {
        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public int CartId { get; set; }

        public bool Isnew { get; set; }

        public bool IsDelete { get; set; }

    }
}
