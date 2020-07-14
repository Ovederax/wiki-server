﻿using System;
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
        // Сохранять новые страницы
        [HttpPost]
        public ActionResult<SuccessResponse> Post(WikiItemCreateRequest req)
        {
            DatabaseContext context = HttpContext.RequestServices
                .GetService(typeof(DatabaseContext)) as DatabaseContext;

            WikiItem item = new WikiItem {
                pageid = 0,
                title = req.title,
                snippet = req.snippet,
                timestamp = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ssZ")
            };
            context.InsertWikiItem(item);
            return new SuccessResponse();
        }

        [HttpPut]
        public ActionResult<SuccessResponse> Put(WikiItemEditRequest req)
        {
            DatabaseContext context = HttpContext.RequestServices
                .GetService(typeof(DatabaseContext)) as DatabaseContext;

            WikiItem item = new WikiItem
            {
                pageid = req.pageid,
                title = req.title,
                snippet = req.snippet, 
                timestamp = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ssZ")
            };
            context.UpdateWikiItem(item);
            return new SuccessResponse();
        }

        [HttpDelete("{pageid}")]
        public ActionResult<SuccessResponse> Delete(int pageid)
        {
            DatabaseContext context = HttpContext.RequestServices
                .GetService(typeof(DatabaseContext)) as DatabaseContext;
            context.DeleleWikiItemById(pageid);
            return new SuccessResponse();
        }
    }
}