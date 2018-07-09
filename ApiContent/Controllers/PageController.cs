using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ApiContent.DataAccess;
using ApiContent.Models;
using ApiContent.Models.DTOs;
using ApiContent.Services;

namespace ApiContent.Controllers
{
    [RoutePrefix("api/Pages")]
    public class PageController : ApiController
    {
        private readonly IPageData _pageRepo = null;

        public PageController()
        {
            _pageRepo = new PageData();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Add(PageDTO page)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = await _pageRepo.AddPage(page);
            return Ok(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Update(PageDTO page)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _pageRepo.UpdatePage(page);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id}/contents")]
        public async Task<IHttpActionResult> UpdateContents(int id, List<IncludedItemDTO> contents)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _pageRepo.UpdatePageContents(id, contents)) return Ok();
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var page = await _pageRepo.GetPage(id);
            if (page == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(page);
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<IHttpActionResult> GetAll(int id = -1, string url = null, bool includeMarks = false, string language = null )
        {
            PageDTO page;
            if (id > 0)
            {
                page = await _pageRepo.GetPageAll(id, includeMarks, language);
            }
            else
            {
                page = await _pageRepo.GetPageByUrl(url, includeMarks, language);
            }
            if (page == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(page);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("menu")]
        public async Task<IHttpActionResult> Add(PageMenuItem menu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = await _pageRepo.AddOrUpdateMenuItem(menu);
            return Ok(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("menu")]
        public async Task<IHttpActionResult> Update(PageMenuItem menu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _pageRepo.AddOrUpdateMenuItem(menu);
            return Ok();
        }

        [HttpGet]
        [Route("menu/{id}")]
        public async Task<IHttpActionResult> GetMenu(int id)
        {
            var menu = await _pageRepo.GetMenuItem(id);
            if (menu == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(menu);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("menu")]
        public async Task<IHttpActionResult> GetMenus(int parentId=-1, bool includeChildren=true)
        {
            var menu = await _pageRepo.GetMenuItems(parentId, includeChildren);
            return Ok(menu);
        }

    }
}
