using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wiki_server.dto.response
{
    public class PageResponse<T>
    {
        public PageResponse(IList<T> content, int page, int pageSize, int totalItems)
        {
            Page = page;
            PageSize = pageSize;
            TotalItems = totalItems;
            TotalPages = totalItems / pageSize + ((totalItems % pageSize > 0) ? 1 : 0);
            ItemsCount = content.Count;
            Content = content;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int ItemsCount { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalItems { get; private set; }



        public IList<T> Content { get; private set; }
    }
}