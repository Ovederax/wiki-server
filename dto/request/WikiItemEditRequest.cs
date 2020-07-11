using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wiki_server.dto.request
{
    public class WikiItemEditRequest
    {
        public int pageid { get; set; }
        public string title { get; set; }
        public string snippet { get; set; }

        public WikiItemEditRequest() { }

        public WikiItemEditRequest(int pageid, string title, string snippet)
        {
            this.pageid = pageid;
            this.title = title;
            this.snippet = snippet;
        }
    }
}
