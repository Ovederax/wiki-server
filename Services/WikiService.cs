using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wiki_server.dto.response;
using wiki_server.Models;

namespace wiki_server.Services
{
    public interface WikiService {
        WikiResponse FindPages(int page, int limit, bool last);
        WikiResponse FindPageByContainText(string text, int page, int limit, bool last);
        bool InsertWikiItem(WikiItem item);
        bool UpdateWikiItem(WikiItem item);
        bool DeleteWikiItem(WikiItem item);
        WikiItem FindWikiItemById(int pageid);
    }

    class WikiServiceImpl : WikiService
    {
        private ApplicationContext ctx;

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
       
        public WikiResponse FindPages(int page, int limit, bool last)
        {
            IQueryable<WikiItem> query = ctx.Items;
            int count = query.Count();
            int allPages = count / limit + ((count % limit > 0)?  1:0);
            if(last) {
                page = allPages-1;
                if(page < 0) {
                    page = 0;
                }
            }
            List<WikiItem> list =  query.Skip(page * limit).Take(limit).ToList();
            List<SearchItem> searchItems = new List<SearchItem>();
            foreach (WikiItem it in list) {
                searchItems.Add(new SearchItem(it.pageid, it.title, it.snippet, it.timestamp));
            }
            return new WikiResponse(page, allPages, searchItems);
        }

        public WikiResponse FindPageByContainText(string text, int page, int limit, bool last)
        {
            var query = ctx.Items.Select(it => it)
                .Where(it => it.title.ToLower().Contains(text.ToLower()));
            int count = query.Count();
            int allPages = count / limit + ((count % limit > 0) ? 1 : 0);
            if (last) {
                page = allPages - 1;
                if (page < 0) {
                    page = 0;
                }
            }
            List<WikiItem> list = query.Skip(page*limit).Take(limit).ToList();
            List<SearchItem> searchItems = new List<SearchItem>();
            foreach (WikiItem it in list) {
                searchItems.Add(new SearchItem(it.pageid, it.title, it.snippet, it.timestamp));
            }
            return new WikiResponse(page, allPages, searchItems);
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
