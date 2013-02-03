using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicStore.Api
{
    public class DeleteItemFromCartCommand:ICommand
    {
        public int CartId { get; set; }

        public int ProductId { get; set; }
    }
}
