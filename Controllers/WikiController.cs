using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wiki_server.dto.response;
using wiki_server.Models;




// Попробуйте вынести логику из контроллеров в сервисы и\или репозитории


namespace wiki_server.Controllers
{
    [ApiController]
    [Route("api/wiki")]
    public class WikiController : ControllerBase
    {
        [HttpGet]
        public ActionResult<WikiResponse> Get()
        {
            DatabaseContext context = HttpContext.RequestServices
                .GetService(typeof(Models.DatabaseContext)) as DatabaseContext;
            List<WikiItem> list =  context.FindAllPages();
            List<SearchItem> searchItems = new List<SearchItem>();
            foreach(WikiItem it in list) {
                searchItems.Add(new SearchItem(it.pageid, it.title, it.snippet, it.timestamp));
            }
            return new WikiResponse(searchItems);
            //return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }


        [HttpGet("{text}")]
        public ActionResult<WikiResponse> Get(string text)
        {
            DatabaseContext context = HttpContext.RequestServices
                .GetService(typeof(Models.DatabaseContext)) as DatabaseContext;
            List<WikiItem> list = context.FindPageByContainText(text);
            List<SearchItem> searchItems = new List<SearchItem>();
            foreach (WikiItem it in list)
            {
                searchItems.Add(new SearchItem(it.pageid, it.title, it.snippet, it.timestamp));
            }
            return new WikiResponse(searchItems);
            //return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }

    }
}