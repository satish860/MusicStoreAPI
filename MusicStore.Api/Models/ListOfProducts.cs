using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.Api.Models
{
    public class ListOfProducts
    {
        public IEnumerable<ProductViewModel> Products { get; set; }

        public int PageCount { get; set; }

    }
}