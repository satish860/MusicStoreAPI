using MusicStore.Api.Controllers;
using MusicStore.Api.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.UnitTest
{
    public static class TestInstance
    {
        public static dynamic ProductQuery = new GetAllProductQuery();
        public static dynamic CategoryCountQuery = new CategoryAndCountQuery();
        public static dynamic ProductsByCategoryQuery = new ProductByCategoryName();
        public static dynamic ProductsController = new ProductsController(ProductQuery,ProductsByCategoryQuery);
        public static dynamic CategoryController = new CategoryController(CategoryCountQuery);
        public static dynamic ProductController = null;
    }
}
