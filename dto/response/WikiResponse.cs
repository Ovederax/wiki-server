using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wiki_server.dto.response
{
    public class SearchItem
    {
        public int ns { get; set; }
        public string title { get; set; }
        public int pageid { get; set; }
        public int size { get; set; }
        public int wordcount { get; set; }
        public string snippet { get; set; }
        public string timestamp { get; set; }

        public SearchItem(int pageid, string title, string snippet, string timestamp)
        {
            this.pageid = pageid;
            this.title = title;
            this.snippet = snippet;
            this.timestamp = timestamp;
        }
    }
    public class QueryItem {
        public List<SearchItem> search { get; set; }
    }
    

    public struct WikiResponse {
        public QueryItem query { get; set; }

        public WikiResponse(List<SearchItem> searchItems) : this()
        {
            this.query = new QueryItem() { search = searchItems };
        }
    }
}
