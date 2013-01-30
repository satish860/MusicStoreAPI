using MusicStore.Api.Models;
using MusicStore.Api.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicStore.Api.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IQueryFor<PagedResult, ListOfProducts> ProductQuery;
        public ProductsController(IQueryFor<PagedResult, ListOfProducts> Query)
        {
            this.ProductQuery = Query;
        }

        public ListOfProducts Get(int PageNumber = 1)
        {
            ListOfProducts products = this.ProductQuery.Execute(PagedResult.ForPage(PageNumber));
            string link = Url.Link("DefaultApi", new { controller = "Cart", Id = "REPLACEID"});
            products.Products.ToList().ForEach(product =>
            {
                product.AddToCartUrl=link.Replace("REPLACEID", product.Id.ToString());
            });
            return products;
        }
    }
}
