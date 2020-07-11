using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wiki_server.dto.request
{
    public class WikiItemCreateRequest
    {
        public string title { get; set; }
        public string snippet { get; set; }

        public WikiItemCreateRequest() { }

        public WikiItemCreateRequest(string title, string snippet)
        {
            this.title = title;
            this.snippet = snippet;
        }
    }
}
