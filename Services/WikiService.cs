using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wiki_server.Models;

namespace wiki_server.Services
{
    public interface WikiService {
        List<WikiItem> FindAllPages();
        List<WikiItem> FindPageByContainText(string text);
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

        public List<WikiItem> FindAllPages()
        {
            return ctx.Items.ToList();
        }

        public List<WikiItem> FindPageByContainText(string text)
        {
            var query = ctx.Items.Select(it => it)
                .Where(it => it.title.Contains(text));
            return query.ToList();
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
