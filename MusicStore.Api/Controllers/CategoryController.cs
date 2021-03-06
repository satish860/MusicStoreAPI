﻿using MusicStore.Api.Query;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using MusicStore.Api.Models;

namespace MusicStore.Api.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly IQueryFor<NullArgument, IEnumerable<CategoryCountViewModel>> Query;

        public CategoryController(IQueryFor<NullArgument, IEnumerable<CategoryCountViewModel>> Query)
        {
            this.Query = Query;
        }

        public IEnumerable<CategoryCountViewModel> Get()
        {
            IEnumerable<CategoryCountViewModel> categoryByCount = this.Query.Execute(NullArgument.BuildDefault());
            string link = Url.Link("ActionApi", new { controller = "Products", action = "GetByCategory", Id = "REPLACEID" });
            categoryByCount.ToList().ForEach(category =>
            {
                category.ProductUrl = link.Replace("REPLACEID", category.CategoryName);
            });
            return categoryByCount;
        }
    }
}