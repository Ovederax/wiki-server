using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using wiki_server.dto.request;
using wiki_server.dto.response;
using wiki_server.Models;
using wiki_server.Services;

namespace wiki_server.Controllers
{
    [Route("api/wiki")]
    [ApiController]
    public class WikiEditController : ControllerBase
    {
        private WikiService service;

        public WikiEditController(WikiService service)
        {
            this.service = service;
        }

        // Сохранять новые страницы
        [HttpPost]
        public ActionResult<SuccessResponse> Post(WikiItemCreateRequest req)
        {
            WikiItem item = new WikiItem {
                title = req.title,
                snippet = req.snippet,
                timestamp = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ssZ")
            };
            service.InsertWikiItem(item);
            return new SuccessResponse();
        }

        [HttpPut]
        public ActionResult<SuccessResponse> Put(WikiItemEditRequest req)
        {
            WikiItem item = service.FindWikiItemById(req.pageid);
            item.title = req.title;
            item.snippet = req.snippet;
            item.timestamp = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ssZ");
            service.UpdateWikiItem(item);
            return new SuccessResponse();
        }

        [HttpDelete("{pageid}")]
        public ActionResult<SuccessResponse> Delete(int pageid)
        {
            WikiItem item = service.FindWikiItemById(pageid);
            service.DeleteWikiItem(item);
            return new SuccessResponse();
        }
    }
}