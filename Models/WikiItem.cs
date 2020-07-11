using System;
using System.ComponentModel.DataAnnotations;

namespace wiki_server.Models
{
    public class WikiItem
    {
        private DatabaseContext context;
        [Key]
        public int pageid { get; set; }
        public string title { get; set; }
        public string snippet { get; set; }
        public string timestamp { get; set; }

    }
}