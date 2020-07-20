using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult<PageResponse<SearchItem>> GetPages(int page, int pageSize)
        {
            if(page <= 0) {
                page = 0;
            }
            if(pageSize <= 0) {
                pageSize = 10;
            }
            return service.FindPages(page, pageSize);
        }


        [HttpGet("{text}")]
        public ActionResult<PageResponse<SearchItem>> GetPagesByTitle(string text, int page, int pageSize)
        {
            if (page <= 0) {
                page = 0;
            }
            if (pageSize <= 0) {
                pageSize = 10;
            }
            return service.FindPageByContainText(text, page, pageSize);
        }
    }
}