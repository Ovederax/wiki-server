using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wiki_server.dto.response;
using wiki_server.Models;
using wiki_server.Services;




// TODO Попробуйте вынести логику из контроллеров в сервисы и\или репозитории

namespace wiki_server.Controllers
{
    [ApiController]
    [Route("api/wiki")]
    public class WikiController : ControllerBase
    {
        private WikiService service;

        public WikiController(WikiService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<WikiResponse> Get()
        {
            List<WikiItem> list = service.FindAllPages();
            List<SearchItem> searchItems = new List<SearchItem>();
            foreach(WikiItem it in list) {
                searchItems.Add(new SearchItem(it.pageid, it.title, it.snippet, it.timestamp));
            }
            return new WikiResponse(searchItems);
        }


        [HttpGet("{text}")]
        public ActionResult<WikiResponse> Get(string text)
        {
            List<WikiItem> list = service.FindPageByContainText(text);
            List<SearchItem> searchItems = new List<SearchItem>();
            foreach (WikiItem it in list)
            {
                searchItems.Add(new SearchItem(it.pageid, it.title, it.snippet, it.timestamp));
            }
            return new WikiResponse(searchItems);
        }
    }
}