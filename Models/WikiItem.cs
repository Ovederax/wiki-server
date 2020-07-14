using System;
using System.ComponentModel.DataAnnotations;
using wiki_server.Services;

namespace wiki_server.Models
{
    public class WikiItem
    {
        [Key]
        public int pageid { get; set; }
        public string title { get; set; }
        public string snippet { get; set; }
        public string timestamp { get; set; }

    }
}