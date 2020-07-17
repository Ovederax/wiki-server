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
        public ActionResult<WikiResponse> Get(int page, int limit, bool last)
        {
            if(page <= 0) {
                page = 0;
            }
            if(limit <= 0 || limit > 10) {
                limit = 10;
            }

            return service.FindPages(page, limit, last);
        }


        [HttpGet("{text}")]
        public ActionResult<WikiResponse> Get(string text, int page, int limit, bool last)
        {
            if (page <= 0) {
                page = 0;
            }
            if (limit <= 0 || limit > 10) {
                limit = 10;
            }

            return service.FindPageByContainText(text, page, limit, last);
        }
    }
}