using MusicStore.Api.Models;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.Api.Query
{
    public class GetAllProductQuery : IQueryFor<PagedResult, ListOfProducts>
    {
        public ListOfProducts Execute(PagedResult input)
        {
            Future<int> Count;
            var Db = Database.Open();
            IEnumerable<ProductViewModel> products = Db.Products.FindAll(Db.Products.Id != 0).
                                                     WithTotalCount(out Count)
                                                     .Skip((input.PageNumber - 1) * 10)
                                                     .Take(10);
            var pageCount = (Count / 10) + ((Count % 10) != 0 ? 1 : 0);
            ListOfProducts productsList = new ListOfProducts { PageCount = pageCount, Products = products.ToList() };
            return productsList;
        }
    }
}