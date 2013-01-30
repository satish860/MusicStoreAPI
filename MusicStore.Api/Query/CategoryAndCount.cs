using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.Api.Query
{
    public class CategoryAndCountQuery : IQueryFor<NullArgument, IEnumerable<CategoryCountViewModel>>
    {
        private Action<CategoryCountViewModel> Categorymap;
        private IList<CategoryCountViewModel> ViewModels = null;

        public CategoryAndCountQuery()
        {
            ViewModels = new List<CategoryCountViewModel>();
            Categorymap = (category) =>
            {
                var CategoryinList = ViewModels.FirstOrDefault(p => p.CategoryName == category.CategoryName);
                if (CategoryinList != null)
                    CategoryinList.Count++;
                else
                {
                    category.Count = 1;
                    ViewModels.Add(category);
                }
            };
        }

        public IEnumerable<CategoryCountViewModel> Execute(NullArgument input)
        {
            var db = Database.Open();
            IEnumerable<CategoryCountViewModel> CategoryViewModels = db.Products.Select(db.Products.Categories.As("CategoryName"));
            CategoryViewModels.ToList().ForEach(p => Categorymap(p));
            return ViewModels;
        }
    }
}