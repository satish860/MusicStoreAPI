using MusicStore.Api.Models;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.Api.Query
{
    public class ProductByCategoryName:IQueryFor<CategoryName,ListOfProducts>
    {
        public ListOfProducts Execute(CategoryName input)
        {
            Future<int> Count;
            var Db = Database.Open();
            IEnumerable<ProductViewModel> products = Db.Products.FindAllByCategories(input.Name).WithTotalCount(out Count);
            var pageCount = (Count / 10) + ((Count % 10) != 0 ? 1 : 0);
            ListOfProducts productsList = new ListOfProducts { Products = products,PageCount=pageCount };
            return productsList;
        }
    }
}