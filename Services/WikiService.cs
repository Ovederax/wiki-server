using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wiki_server.dto.response;
using wiki_server.Models;

namespace wiki_server.Services
{
    public interface WikiService {
        PageResponse<SearchItem> FindPages(int page, int pageSize);
        PageResponse<SearchItem> FindPageByContainText(string text, int page, int pageSize);
        bool InsertWikiItem(WikiItem item);
        bool UpdateWikiItem(WikiItem item);
        bool DeleteWikiItem(WikiItem item);
        WikiItem FindWikiItemById(int pageid);
    }

    class WikiServiceImpl : WikiService
    {
        private ApplicationContext ctx;
        private const int LAST_PAGE = Int32.MaxValue;

        public WikiServiceImpl(ApplicationContext ctx) {
            this.ctx = ctx;
        }

        public bool InsertWikiItem(WikiItem item)
        {
            ctx.Items.Add(item);
            ctx.SaveChanges();
            return true;
        }

        public bool DeleteWikiItem(WikiItem item)
        {
            ctx.Items.Remove(item);
            ctx.SaveChanges();
            return true;
        }
       
        public PageResponse<SearchItem> FindPages(int page, int pageSize)
        {
            IQueryable<WikiItem> query = ctx.Items;
            int totalItems = query.Count();
            int totalPages = totalItems / pageSize + ((totalItems % pageSize > 0)?  1:0);
            if(page == LAST_PAGE) {
                page = totalPages-1;
                if(page < 0) {
                    page = 0;
                }
            }
            List<WikiItem> list =  query.Skip(page * pageSize).Take(pageSize).ToList();
            List<SearchItem> searchItems = new List<SearchItem>();
            foreach (WikiItem it in list) {
                searchItems.Add(new SearchItem(it.pageid, it.title, it.snippet, it.timestamp));
            }
            return new PageResponse<SearchItem>(searchItems, page, pageSize, totalItems);
        }

        public PageResponse<SearchItem> FindPageByContainText(string text, int page, int pageSize)
        {
            var query = ctx.Items.Select(it => it)
                .Where(it => it.title.ToLower().Contains(text.ToLower()));
            int totalItems = query.Count();
            int totalPages = totalItems / pageSize + ((totalItems % pageSize > 0) ? 1 : 0);
            if (page == LAST_PAGE) {
                page = totalPages - 1;
                if (page < 0) {
                    page = 0;
                }
            }
            List<WikiItem> list = query.Skip(page*pageSize).Take(pageSize).ToList();
            List<SearchItem> searchItems = new List<SearchItem>();
            foreach (WikiItem it in list) {
                searchItems.Add(new SearchItem(it.pageid, it.title, it.snippet, it.timestamp));
            }
            return new PageResponse<SearchItem>(searchItems, page, pageSize, totalItems);
        }

        public bool UpdateWikiItem(WikiItem item)
        {
            ctx.Items.Update(item);
            return true;
        }

        public WikiItem FindWikiItemById(int pageid)
        {
            return ctx.Items.Find(pageid);
        }
    }
}
