using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicStore.Api
{
    public class AddProductToCartCommand:ICommand
    {
        public int CartId { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }
    }
}
