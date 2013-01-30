using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicStore.Api
{
    public class PagedResult
    {
        public readonly int PageNumber;
        public PagedResult(int PageNumber)
        {
            this.PageNumber = PageNumber;
        }

        public static PagedResult ForPage(int Number)
        {
            return new PagedResult(Number);
        }
    }
}
